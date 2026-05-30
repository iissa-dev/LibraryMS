namespace LibraryMS.Application.Features.Author.Commands.Restore;

public sealed class RestoreAuthorCommandValidator : AbstractValidator<RestoreAuthorCommand>
{
    public RestoreAuthorCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Invalid author ID.")
            .NotEmpty()
            .WithMessage("Author ID is required.");
    }
}