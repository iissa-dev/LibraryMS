using LibraryMS.Application.Common.DTOs.AuthDto;
using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Auth.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<Result<TokenResult>>;
