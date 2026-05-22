using LibraryMS.Application.DTOs.AuthDto;
using LibraryMS.Application.Results;
using MediatR;

namespace LibraryMS.Application.Features.Auth.Commands.Login;

public sealed record LoginCommand(string Username, string Password) : IRequest<Result<TokenResult>>;
