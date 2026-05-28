using FluentValidation;

namespace LibraryMS.Application.Features.Employee.Queries.GetEmployeeById;

public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
{
    public GetEmployeeByIdQueryValidator()
    {
        RuleFor(x => x.UserId)
        .NotEmpty().WithMessage("User Id is required")
        .GreaterThan(0).WithMessage("User Id must be a valid positive number.");
    }
}