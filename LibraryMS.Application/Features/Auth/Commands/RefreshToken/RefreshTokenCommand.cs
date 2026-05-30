using LibraryMS.Application.DTOs.AuthDto;

namespace LibraryMS.Application.Features.Auth.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<Result<TokenResult>>;
