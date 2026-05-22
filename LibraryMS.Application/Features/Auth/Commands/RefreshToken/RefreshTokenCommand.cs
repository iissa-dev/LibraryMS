using LibraryMS.Application.DTOs.AuthDto;
using LibraryMS.Application.Results;
using MediatR;

namespace LibraryMS.Application.Features.Auth.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<Result<TokenResult>>;
