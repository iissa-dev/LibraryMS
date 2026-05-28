using FluentValidation;

namespace LibraryMS.Application.Features.Employee.Queries.GetAllEmployee;

public class GetAllEmployeeQueryValidator : AbstractValidator<GetAllEmployeeQuery>
{
    public GetAllEmployeeQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("PageNumber must be greater than zero");
        RuleFor(x => x.PageSize).GreaterThan(0).LessThan(100).WithMessage("PageSize must be greater than zero");
    }
}