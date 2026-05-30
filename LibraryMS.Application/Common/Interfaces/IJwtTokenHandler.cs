using LibraryMS.Application.DTOs.AuthDto;

namespace LibraryMS.Application.Common.Interfaces;

public interface IJwtTokenHandler
{
    Task<Result<TokenResult>> GenerateRefreshTokenAsync(string refreshToken);
    Task<Result<TokenResult>> GenerateFullTokenResult(int userId);
}