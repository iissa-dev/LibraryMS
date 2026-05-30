using LibraryMS.Application.DTOs.AuthDto;

namespace LibraryMS.Application.Features.Auth.Queries.Login;

public sealed record LoginQuery(string Username, string Password) : IRequest<Result<TokenResult>>;
