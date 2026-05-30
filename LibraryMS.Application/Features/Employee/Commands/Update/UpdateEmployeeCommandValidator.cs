namespace LibraryMS.Application.Features.Employee.Commands.Update;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty().WithMessage("UserId is required");
        RuleFor(c => c.FirstName).NotEmpty().WithMessage("First Name is required");
        RuleFor(c => c.LastName).NotEmpty().WithMessage("Last Name is required");
        RuleFor(c => c.Address).NotEmpty().WithMessage("Address is required");
        RuleFor(c => c.EmployeeCode).NotEmpty().WithMessage("Employee code is required");
        RuleFor(c => c.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
        RuleFor(c => c.UserName).NotEmpty().WithMessage("Usrename is required");
        RuleFor(c => c.DateOfBirth).NotEmpty().WithMessage("Date of birth is required");
        RuleFor(c => c.CountryId).NotEmpty().WithMessage("Country is required");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}
