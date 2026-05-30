namespace LibraryMS.Application.Features.Employee.Commands.CreateEmployeeAccount;

public sealed class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
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
        RuleFor(c => c.EmployeeCode).NotEmpty().WithMessage("EmployeeCode is required");
        RuleFor(e => e.RoleId).Must(role => Enum.IsDefined(typeof(Roles), (Roles)role))
        .WithMessage("Invalid Role Selected");
    }
}