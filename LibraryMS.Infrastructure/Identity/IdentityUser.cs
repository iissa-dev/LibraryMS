using LibraryMS.Application.Common.DTOs.AuthDto;
using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Application.Common.Results;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Infrastructure.Identity;

public class IdentityUser : IIdentityUser
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenHandler _jwtTokenHandler;

    public IdentityUser(UserManager<ApplicationUser> userManager, IJwtTokenHandler jwtTokenHandler,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _jwtTokenHandler = jwtTokenHandler;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> CreateUserAsync(string email, string password, string username, int personId,
        string? phoneNumber)
    {
        var userExists = await _userManager.FindByEmailAsync(email);
        if (userExists != null)
        {
            return Result<int>.Failure("This email address is already registered.");
        }

        var user = new ApplicationUser
        {
            Email = email,
            UserName = username,
            PersonId = personId,
            PhoneNumber = phoneNumber ?? ""
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var firstError = result.Errors.FirstOrDefault()?.Description ?? "User creation failed.";
            return Result<int>.Failure(firstError);
        }

        return Result<int>.Success(user.Id);
    }

    public async Task<Result<int>> AddUserToRoleAsync(string username, Roles role)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null) return Result<int>.Failure("User not found.");

        var result = await _userManager.AddToRoleAsync(user, role.ToString());

        if (!result.Succeeded)
        {
            var firstError = result.Errors.FirstOrDefault()?.Description ?? "Role creation failed.";
            return Result<int>.Failure(firstError);
        }

        return Result<int>.Success(user.Id);
    }

    public async Task<Result<TokenResult>> LoginAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user is null || !await _userManager.CheckPasswordAsync(user, password))
            return Result<TokenResult>.Failure("Invalid username or password.");

        var tokenResult = await _jwtTokenHandler.GenerateFullTokenResult(user.Id);
        await _unitOfWork.SaveChangesAsync();

        return tokenResult;
    }

    private async Task<Result> RevokeTokenAsync(string refreshToken)
    {
        var token = await _unitOfWork.RefreshTokens.GetByTokenAsync(refreshToken);

        if (token is null) return Result.Failure("Token not found");

        if (token.IsRevoked) return Result.Failure("Token is already revoked");

        token.IsRevoked = true;
        token.RevokedAt = DateTime.UtcNow;

        _unitOfWork.RefreshTokens.Update(token);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success;
    }

    public async Task<Result> Logout(string refreshToken)
        => await RevokeTokenAsync(refreshToken);

    public async Task<Result<TokenResult>> RefreshTokenAsync(string refreshToken)
        => await _jwtTokenHandler.GenerateRefreshTokenAsync(refreshToken);

    public async Task<Result<CurrentUserDto>> CurrentUserByIdAsync(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) return Result<CurrentUserDto>.Failure("User not found");
        
        var person = await _unitOfWork.Repository<Person>()
        .Query()
        .Include(p => p.Country)
        .FirstOrDefaultAsync(p =>  p.Id == user.PersonId!.Value);

        if(person is null) return Result<CurrentUserDto>.Failure("Person not found"); 

        return Result<CurrentUserDto>.Success(new CurrentUserDto
        {
            UserId = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Address = person.Address,
            PhoneNumber = user.PhoneNumber,
            ImageUrl = person.ImageUrl,
            DateOfBirth = person.DateOfBirth,
            Country = person.Country.Name
        });
    }

    public async Task<Result<int>> AddToRolesAsync(string username, IEnumerable<string> roles)
    {
        var user = await _userManager.FindByNameAsync(username);
        if(user is null) return Result<int>.Failure("User not found");

        var result = await _userManager.AddToRolesAsync(user , roles);
        
        return result.Succeeded 
        ? Result<int>.Success(user.Id) 
        : Result<int>.Failure(result.Errors.FirstOrDefault()?.Description ?? "Role creation failed.");
    }
}