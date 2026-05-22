using LibraryMS.Application.Interfaces.IRepository;
using LibraryMS.Application.Results;
using MediatR;

namespace LibraryMS.Application.Features.Auth.Commands.Logout;

public sealed class LogoutCommandHandler(IIdentityUser identityUser) : IRequestHandler<LogoutCommand, Result>
{
    public Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        return identityUser.Logout(request.RefreshToken);
    }
}