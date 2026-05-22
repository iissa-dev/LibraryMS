using LibraryMS.Application.DTOs.AuthDto;
using LibraryMS.Application.Results;
using LibraryMS.Domain.Enums;

namespace LibraryMS.Application.Interfaces.IRepository;

public interface IIdentityUser
{
    Task<Result<int>> CreateUserAsync(string email, string password, string username, int personId, string? phoneNumber);
    Task<Result<int>> AddUserToRoleAsync(string username, Roles role);
    Task<Result<TokenResult>> LoginAsync(string username, string password);
    Task<Result> Logout(string refreshToken);
    Task<Result<TokenResult>> RefreshTokenAsync(string refreshToken);
    Task<Result<CurrentUserDto>> CurrentUserByIdAsync(int userId);
}