using FluentValidation;

namespace LibraryMS.Application.Features.Employee.Commands.Delete;

public sealed class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0).WithErrorCode("InvalidUserId").WithMessage("UserId must be greater than 0.");
    }
}