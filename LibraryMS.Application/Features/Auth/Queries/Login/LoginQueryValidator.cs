namespace LibraryMS.Application.Features.Auth.Queries.Login;

public sealed class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(l => l.Username).NotEmpty().WithMessage("Username is required");

        RuleFor(l => l.Password)
        .NotEmpty().WithMessage("Password is required.")
        .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}
