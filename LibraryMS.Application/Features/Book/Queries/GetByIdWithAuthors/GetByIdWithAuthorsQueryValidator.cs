namespace LibraryMS.Application.Features.Book.Queries.GetByIdWithAuthors;

public sealed class GetByIdWithAuthorsQueryValidator : AbstractValidator<GetByIdWithAuthorsQuery>
{
    public GetByIdWithAuthorsQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Book ID must be greater than 0.");
    }
}