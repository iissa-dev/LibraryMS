namespace LibraryMS.Application.Features.BookCopies.Queries.GetAllCopies;

public sealed class GetAllCopiesQueryValidator : AbstractValidator<GetAllCopiesQuery>
{
    public GetAllCopiesQueryValidator()
    {
        RuleFor(b => b.BookId).NotEmpty().WithMessage("Book id is required");
        RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("PageNumber must be greater than zero");
        RuleFor(x => x.PageSize).GreaterThan(0).LessThan(100).WithMessage("PageSize must be greater than zero");
    }
}