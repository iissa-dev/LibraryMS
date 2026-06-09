namespace LibraryMS.Application.Features.Fine.Queries.GetById;

public sealed class GetAllfinesByIdQueryValidator : AbstractValidator<GetAllFinesByIdQuery>
{
    public GetAllfinesByIdQueryValidator()
    {
        RuleFor(f => f.PageNumber).GreaterThan(0).WithMessage("Page number should be greater then zero");
        RuleFor(f => f.PageSize).LessThanOrEqualTo(100).WithMessage("Page size should be less than 100");
        RuleFor(f => f.ClientId).GreaterThan(0).WithMessage("Client ID must be a valid positive integer.");
    }
}