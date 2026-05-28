using LibraryMS.Application.Common.Results;
using LibraryMS.Application.DTOs.AuthDto;
using MediatR;

namespace LibraryMS.Application.Features.Auth.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : IRequest<Result<TokenResult>>;
