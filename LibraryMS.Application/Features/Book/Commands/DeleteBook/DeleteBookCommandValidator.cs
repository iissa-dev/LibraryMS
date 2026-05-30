using FluentValidation;

namespace LibraryMS.Application.Features.Book.Commands.DeleteBook;

public sealed class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(x => x.Id)
        .GreaterThan(0)
        .WithMessage("Invalid book ID.")
        .NotEmpty()
        .WithMessage("Book ID is required.");
    }
}