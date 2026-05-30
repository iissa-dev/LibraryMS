namespace LibraryMS.Application.Features.Book.Queries.GetAllBook;

public sealed class GetAllBooksQueryValidator : AbstractValidator<GetAllBooksQuery>
{
    public GetAllBooksQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("PageNumber must be greater than zero");
        RuleFor(x => x.PageSize).GreaterThan(0).LessThan(100).WithMessage("PageSize must be greater than zero");
    }
}