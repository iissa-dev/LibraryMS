namespace LibraryMS.Application.Features.Auth.Commands.Logout;

public sealed class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(l => l.RefreshToken).NotEmpty().WithMessage("Refresh token is required");

    }
}
