namespace LibraryMS.Application.Features.BookCopies.Command.Create;

public sealed class CreateBookCopyCommandValidator : AbstractValidator<CreateBookCopyCommand>
{
    public CreateBookCopyCommandValidator()
    {
        RuleFor(bc => bc.InitialCopiesCount)
        .GreaterThan(0)
        .WithMessage("Copies count must be greater than zero.")
        .LessThanOrEqualTo(100)
        .WithMessage("You cannot create more than 100 copies at once.");
    }
}