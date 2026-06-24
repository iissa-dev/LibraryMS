namespace LibraryMS.Application.Features.Book.Commands.CreateBook;

public sealed class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.PublishDate)
            .NotEmpty().WithMessage("PublishDate is required")
            .LessThan(DateTime.Now).WithMessage("PublishDate cannot be in the future");

        RuleFor(x => x.Genre)
            .Must(genre => Enum.IsDefined(typeof(Genre), (Genre)genre))
            .WithMessage("Invalid Genre selected.");

        RuleFor(x => x.AdditionalDetails).NotEmpty().WithMessage("AdditionalDetails is required");

        RuleFor(x => x.AuthorIds)
            .NotEmpty().WithMessage("At least one author is is required")
            .Must(authorIds => authorIds != null && authorIds.All(id => id > 0))
            .WithMessage("Invalid Author Id detected.");

        RuleFor(x => x.InitialCopiesCount)
            .GreaterThanOrEqualTo(0).WithMessage("InitialCopiesCount cannot be negative")
            .LessThanOrEqualTo(100).WithMessage("InitialCopiesCount cannot exceed 100")
            .Must(count => count >= 1 || count == 0).WithMessage("InitialCopiesCount must be at least 1 if provided.");
    }
}