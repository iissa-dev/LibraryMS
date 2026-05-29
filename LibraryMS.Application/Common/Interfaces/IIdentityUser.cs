using LibraryMS.Application.Common.Results;
using LibraryMS.Application.DTOs.AuthDto;
using LibraryMS.Application.DTOs.UserDto;
using LibraryMS.Domain.Enums;

namespace LibraryMS.Application.Common.Interfaces;

public interface IIdentityUser
{
    Task<Result<int>> CreateUserAsync(string email, string password, string username, string? phoneNumber,
     string firstName, string lastName, string address, int countryId, DateOnly dateOfBirth);
    Task<Result<int>> AddUserToRoleAsync(string username, Roles role);
    Task<Result<TokenResult>> LoginAsync(string username, string password);
    Task<Result> Logout(string refreshToken);
    Task<Result<TokenResult>> RefreshTokenAsync(string refreshToken);
    Task<Result<CurrentUserDto>> CurrentUserByIdAsync(int userId);
    Task<Result<int>> AddToRolesAsync(string username, IEnumerable<string> roles);
    Task<Result> UpdateUserInfoAsync(UpdateUserInfoDto dto);

    Task<Result> DeleteUserAsync(int UserId);

    Task<Result> RestoreUserAsync(int userId);
}