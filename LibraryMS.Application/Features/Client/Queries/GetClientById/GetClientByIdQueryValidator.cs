namespace LibraryMS.Application.Features.Client.Queries.GetClientById;

public class GetClientByIdQueryValidator : AbstractValidator<GetClientByIdQuery>
{
    public GetClientByIdQueryValidator()
    {
        RuleFor(x => x.UserId)
        .NotEmpty().WithMessage("User Id is required")
        .GreaterThan(0).WithMessage("User Id must be a valid positive number.");
    }
}