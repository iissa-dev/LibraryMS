using LibraryMS.Application.DTOs.AuthDto;
using LibraryMS.Application.Results;

namespace LibraryMS.Application.Interfaces.IRepository;

public interface IJwtTokenHandler
{
    Task<Result<TokenResult>> GenerateRefreshTokenAsync(string refreshToken);
    Task<Result<TokenResult>> GenerateFullTokenResult(int userId);
}