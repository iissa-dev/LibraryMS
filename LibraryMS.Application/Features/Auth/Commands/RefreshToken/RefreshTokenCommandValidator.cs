using FluentValidation;

namespace LibraryMS.Application.Features.Auth.Commands.RefreshToken;

public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(l => l.RefreshToken).NotEmpty().WithMessage("Refresh token is required");

    }
}
