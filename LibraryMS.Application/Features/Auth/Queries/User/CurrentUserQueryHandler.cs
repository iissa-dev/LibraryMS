using LibraryMS.Application.DTOs.AuthDto;

namespace LibraryMS.Application.Features.Auth.Queries.User;

public class CurrentUserQueryHandler(IIdentityUser identityUser) : IRequestHandler<CurrentUserQuery, Result<CurrentUserDto>>
{
    public async Task<Result<CurrentUserDto>> Handle(CurrentUserQuery request, CancellationToken cancellationToken)
    {
        return await identityUser.CurrentUserByIdAsync(request.UserId);
    }
}