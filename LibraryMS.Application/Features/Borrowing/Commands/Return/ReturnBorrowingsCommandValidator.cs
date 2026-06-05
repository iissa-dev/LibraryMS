namespace LibraryMS.Application.Features.Borrowing.Commands.Return;

public sealed class ReturnBorrowingsCommandValidator : AbstractValidator<ReturnBorrowingsCommand>
{
    public ReturnBorrowingsCommandValidator()
    {
        RuleFor(b => b.BorrowingId)
        .GreaterThan(0)
        .WithMessage("Borrowing Id must be a valid ID greater than 0");

        RuleFor(b => b.CopyId)
        .GreaterThan(0)
        .WithMessage("Copy Id must be a valid ID greater than 0");
    }
}