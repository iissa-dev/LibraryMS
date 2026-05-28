using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Auth.Commands.Logout;

public sealed class LogoutCommandHandler(IIdentityUser identityUser) : IRequestHandler<LogoutCommand, Result>
{
    public Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        return identityUser.Logout(request.RefreshToken);
    }
}