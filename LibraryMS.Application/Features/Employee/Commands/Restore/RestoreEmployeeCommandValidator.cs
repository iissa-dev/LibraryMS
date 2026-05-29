using FluentValidation;

namespace LibraryMS.Application.Features.Employee.Commands.Restore;

public sealed class RestoreEmployeeCommandValidator : AbstractValidator<RestoreEmployeeCommand>
{
    public RestoreEmployeeCommandValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0).WithMessage("UserId must be greater than 0.");
    }
}