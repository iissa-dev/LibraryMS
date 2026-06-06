namespace LibraryMS.Application.Features.Employee.Queries.GetEmployeeById;

public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
{
    public GetEmployeeByIdQueryValidator()
    {
        RuleFor(x => x.EmployeeId)
        .NotEmpty().WithMessage("Employee Id is required")
        .GreaterThan(0).WithMessage("Employee Id must be a valid positive number.");
    }
}