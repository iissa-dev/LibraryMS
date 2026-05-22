using LibraryMS.Application.DTOs.AuthDto;
using LibraryMS.Application.Interfaces.IRepository;
using LibraryMS.Application.Results;
using MediatR;

namespace LibraryMS.Application.Features.Auth.Commands.Login;

public sealed class LoginCommandHandler(IIdentityUser identityUser) : IRequestHandler<LoginCommand, Result<TokenResult>>
{
    public async Task<Result<TokenResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await identityUser.LoginAsync(request.Username, request.Password);
    }
}