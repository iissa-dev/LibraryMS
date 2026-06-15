namespace LibraryMS.Application.Features.Book.Queries.GetAllBook;

public sealed class GetAllBooksQueryValidator : AbstractValidator<GetAllBooksQuery>
{
    public GetAllBooksQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("PageNumber must be greater than zero");
        RuleFor(x => x.PageSize).GreaterThan(0).LessThan(100).WithMessage("PageSize must be greater than zero");
        RuleFor(x => x.SearchByGenre)
            .Must(genre => Enum.IsDefined(typeof(Genre), (Genre)genre!))
            .WithMessage("Invalid Genre selected")
            .When(x => x.SearchByGenre is not null);
    }
}