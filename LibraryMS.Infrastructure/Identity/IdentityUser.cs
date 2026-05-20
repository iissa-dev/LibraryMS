using LibraryMS.Application.DTOs.AuthDto;
using LibraryMS.Application.Interfaces.IRepository;
using LibraryMS.Application.Result;
using LibraryMS.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Infrastructure.Identity;

public class IdentityUser : IIdentityUser
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtTokenHandler _jwtTokenHandler;

    public IdentityUser(UserManager<ApplicationUser> userManager, JwtTokenHandler jwtTokenHandler,
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

        var tokenResult = await _jwtTokenHandler.GenerateFullTokenResult(user);
        await _unitOfWork.SaveChangesAsync();

        return tokenResult;
    }
}