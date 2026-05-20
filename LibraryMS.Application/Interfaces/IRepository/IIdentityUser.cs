using LibraryMS.Application.Result;
using LibraryMS.Domain.Enums;

namespace LibraryMS.Application.Interfaces.IRepository;

public interface IIdentityUser
{
    Task<Result<int>> CreateUserAsync(string email, string password, string username, int personId, string? phoneNumber);
    Task<Result<int>> AddUserToRoleAsync(string username, Roles role);
}