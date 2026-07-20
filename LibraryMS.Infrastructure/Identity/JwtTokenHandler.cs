using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using LibraryMS.Application.DTOs.AuthDto;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LibraryMS.Infrastructure.Identity;

public class JwtTokenHandler : IJwtTokenHandler
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAppDbContext _context;

    public JwtTokenHandler(IConfiguration configuration,
        UserManager<ApplicationUser> userManager,
        IAppDbContext context)
    {
        _configuration = configuration;
        _userManager = userManager;
        _context = context;
    }

    public async Task<Result<TokenResult>> GenerateFullTokenResult(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null) return Result<TokenResult>.Failure("User not found");

        return await GenerateFullTokenResultInternal(user);
    }

    public async Task<Result<TokenResult>> GenerateRefreshTokenAsync(string refreshToken)
    {
        var token = await _context.RefreshTokens.SingleOrDefaultAsync(t => t.RefreshTokenJwt.Contains(refreshToken));

        if (token is null)
            return Result<TokenResult>.Failure("Refresh token not found");

        if (token.IsRevoked || token.RefreshTokenExpiry < DateTime.UtcNow)
            return Result<TokenResult>.Failure("Refresh token is expired");

        token.IsRevoked = true;
        token.RevokedAt = DateTime.UtcNow;
        _context.RefreshTokens.Update(token);

        var user = await _userManager.FindByIdAsync(token.UserId.ToString());
        if (user is null) return Result<TokenResult>.Failure("User not found");

        var tokenResult = await GenerateFullTokenResultInternal(user);
        await _context.SaveChangesAsync();

        return tokenResult;
    }

    private async Task<Result<TokenResult>> GenerateFullTokenResultInternal(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? nameof(Roles.Client);
        var accessToken = GenerateAccessTokenInternal(user, roles.FirstOrDefault() ?? nameof(Roles.Client));

        var refreshToken = new RefreshToken
        {
            RefreshTokenJwt = Guid.NewGuid().ToString(),
            UserId = user.Id,
            RefreshTokenExpiry = DateTime.UtcNow.AddDays(7)
        };

        _context.RefreshTokens.Add(refreshToken);

        var result = new TokenResult
        {
            UserId = user.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken.RefreshTokenJwt,
            UserName = user.UserName!,
            Role = role,
            PersonId = user.PersonId,
            ClientId = null
        };

        if (role == nameof(Roles.Client))
        {
            var client = await _context
                .Clients.AsNoTracking()
                .FirstOrDefaultAsync(c => c.PersonId == user.PersonId);
            if (client is not null)
                result.ClientId = client.Id;
        }

        return Result<TokenResult>.Success(result);
    }

    private string GenerateAccessTokenInternal(ApplicationUser user, string role)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Role, role),
            new("PersonId", user.PersonId.ToString())
        };

        if (role == nameof(Roles.Client))
        {
            var client = _context.Clients
                .FirstOrDefault(c => c.PersonId == user.PersonId);
            if (client is not null)
                claims.Add(new("ClientId", client.Id.ToString()));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));

        var token = new JwtSecurityToken
        (
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"]!)),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}