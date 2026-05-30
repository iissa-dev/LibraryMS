namespace LibraryMS.Application.Features.Author.Queries.GetAuthorById;

public sealed class GetAuthorByIdQueryValidator : AbstractValidator<GetAuthorByIdQuery>
{
    public GetAuthorByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Author ID must be greater than 0.");
    }
}