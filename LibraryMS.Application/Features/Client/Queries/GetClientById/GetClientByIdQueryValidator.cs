namespace LibraryMS.Application.Features.Client.Queries.GetClientById;

public class GetClientByIdQueryValidator : AbstractValidator<GetClientByIdQuery>
{
    public GetClientByIdQueryValidator()
    {
        RuleFor(x => x.ClientId)
        .NotEmpty().WithMessage("Client Id is required")
        .GreaterThan(0).WithMessage("Client Id must be a valid positive number.");
    }
}