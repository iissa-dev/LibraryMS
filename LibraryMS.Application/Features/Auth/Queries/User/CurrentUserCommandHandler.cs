using LibraryMS.Application.DTOs.AuthDto;
using LibraryMS.Application.Interfaces.IRepository;
using LibraryMS.Application.Results;
using MediatR;

namespace LibraryMS.Application.Features.Auth.Queries.User;

public class CurrentUserCommandHandler(IIdentityUser identityUser) : IRequestHandler<CurrentUserCommand, Result<CurrentUserDto>>
{
    public async Task<Result<CurrentUserDto>> Handle(CurrentUserCommand request, CancellationToken cancellationToken)
    {
        return await identityUser.CurrentUserByIdAsync(request.UserId);
    }
}