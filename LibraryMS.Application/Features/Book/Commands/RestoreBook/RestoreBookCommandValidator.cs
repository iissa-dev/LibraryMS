namespace LibraryMS.Application.Features.Book.Commands.RestoreBook;

public sealed class RestoreBookCommandValidator : AbstractValidator<RestoreBookCommand>
{
    public RestoreBookCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");
    }
}