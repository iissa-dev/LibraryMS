using LibraryMS.Application.Common.Results;
using LibraryMS.Application.DTOs.AuthDto;
using MediatR;

namespace LibraryMS.Application.Features.Auth.Queries.Login;

public sealed record LoginQuery(string Username, string Password) : IRequest<Result<TokenResult>>;
