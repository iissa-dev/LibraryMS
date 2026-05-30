namespace LibraryMS.Application.Features.Client.Commands.RegisterClient;

public sealed class RegisterClientCommandValidator : AbstractValidator<RegisterClientCommand>
{
    public RegisterClientCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        RuleFor(c => c.FirstName).NotEmpty().WithMessage("First name is required");
        RuleFor(c => c.LastName).NotEmpty().WithMessage("Last name is required");
        RuleFor(c => c.Address).NotEmpty().WithMessage("Address is required");
        RuleFor(c => c.UserName).NotEmpty().WithMessage("Username is required");
        RuleFor(c => c.CountryId).NotEmpty().WithMessage("CountryId is required");
        RuleFor(c => c.BirthDate).NotEmpty().WithMessage("BirthDate is required");
        RuleFor(c => c.LibraryCardNumber).NotEmpty().WithMessage("LibraryCardNumber is required");
    }
}