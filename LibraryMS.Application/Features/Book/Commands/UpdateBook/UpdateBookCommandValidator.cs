namespace LibraryMS.Application.Features.Book.Commands.UpdateBook;

public sealed class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(x => x.ISBN)
            .NotEmpty().WithMessage("ISBN is required.")
            .MaximumLength(20).WithMessage("ISBN cannot exceed 20 characters.");

        RuleFor(x => x.PublishDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Publish date cannot be in the future.");

        RuleFor(x => (Genre)x.Genre)
            .IsInEnum().WithMessage("Invalid genre value.");

        RuleFor(x => x.AdditionalDetails)
            .MaximumLength(1000).WithMessage("Additional details cannot exceed 1000 characters.");

        RuleFor(x => x.BookImageUrl)
            .MaximumLength(500).WithMessage("Book image URL cannot exceed 500 characters.");

        RuleFor(x => x.AuthorIds)
            .NotEmpty().WithMessage("At least one author must be selected.");
    }
}