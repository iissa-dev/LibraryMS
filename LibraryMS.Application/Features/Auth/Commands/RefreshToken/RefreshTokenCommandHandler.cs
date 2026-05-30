using LibraryMS.Application.DTOs.AuthDto;

namespace LibraryMS.Application.Features.Auth.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler(IIdentityUser identityUser) : IRequestHandler<RefreshTokenCommand, Result<TokenResult>>
{
    public Task<Result<TokenResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return identityUser.RefreshTokenAsync(request.RefreshToken);
    }
}