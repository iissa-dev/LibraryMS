namespace LibraryMS.Application.Features.Employee.Commands.Update;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(c => c.EmployeeId).NotEmpty().WithMessage("Employee ID is required");
        RuleFor(c => c.EmployeeCode).NotEmpty().WithMessage("Employee code is required");
    }
}
