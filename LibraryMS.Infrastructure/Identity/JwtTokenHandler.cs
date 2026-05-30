using System.IdentityModel.Tokens.Jwt;
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
    private readonly IUnitOfWork _unitOfWork;

    public JwtTokenHandler(IConfiguration configuration, IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TokenResult>> GenerateFullTokenResult(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null) return Result<TokenResult>.Failure("User not found");

        return await GenerateFullTokenResultInternal(user);
    }

    public async Task<Result<TokenResult>> GenerateRefreshTokenAsync(string refreshToken)
    {
        var token = await _unitOfWork.RefreshTokens.GetByTokenAsync(refreshToken);

        if (token is null)
            return Result<TokenResult>.Failure("Refresh token not found");

        if (token.IsRevoked || token.RefreshTokenExpiry < DateTime.UtcNow)
            return Result<TokenResult>.Failure("Refresh token is expired");

        token.IsRevoked = true;
        token.RevokedAt = DateTime.UtcNow;
        _unitOfWork.RefreshTokens.Update(token);

        var user = await _userManager.FindByIdAsync(token.UserId.ToString());
        if (user is null) return Result<TokenResult>.Failure("User not found");

        var tokenResult = await GenerateFullTokenResultInternal(user);
        await _unitOfWork.SaveChangesAsync();

        return tokenResult;
    }

    private async Task<Result<TokenResult>> GenerateFullTokenResultInternal(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var accessToken = GenerateAccessTokenInternal(user, roles.FirstOrDefault() ?? nameof(Roles.Client));
        
        var refreshToken = new RefreshToken
        {
            RefreshTokenJwt = Guid.NewGuid().ToString(),
            UserId = user.Id,
            RefreshTokenExpiry = DateTime.UtcNow.AddDays(7)
        };

        _unitOfWork.RefreshTokens.Add(refreshToken);

        return Result<TokenResult>.Success(new TokenResult
        {
            UserId = user.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken.RefreshTokenJwt,
            UserName = user.UserName!,
            Role = roles.FirstOrDefault() ?? nameof(Roles.Client)
        });
    }

    private string GenerateAccessTokenInternal(ApplicationUser user, string role)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Role, role)
        };

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