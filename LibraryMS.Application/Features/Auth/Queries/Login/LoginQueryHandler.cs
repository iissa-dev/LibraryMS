using LibraryMS.Application.Common.DTOs.AuthDto;
using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Auth.Queries.Login;

public sealed class LoginQueryHandler(IIdentityUser identityUser) : IRequestHandler<LoginQuery, Result<TokenResult>>
{
    public async Task<Result<TokenResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        return await identityUser.LoginAsync(request.Username, request.Password);
    }
}