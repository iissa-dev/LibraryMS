using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Application.Common.Results;
using LibraryMS.Application.DTOs.AuthDto;
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

    public async Task<Result<int>> CreateUserAsync(string email, string password, string username,
        string? phoneNumber, string firstName, string lastName, string address, int countryId, DateOnly dateOfBirth)
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
            PhoneNumber = phoneNumber ?? "",
            FirstName = firstName,
            LastName = lastName,
            Address = address,
            CountryId = countryId,
            DateOfBirth = dateOfBirth
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
        
        return Result<CurrentUserDto>.Success(new CurrentUserDto
        {
            UserId = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Address = user.Address,
            PhoneNumber = user.PhoneNumber,
            ImageUrl = user.ImageUrl,
            DateOfBirth = user.DateOfBirth,
            Country = user.Country?.Name ?? "Not Found"
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