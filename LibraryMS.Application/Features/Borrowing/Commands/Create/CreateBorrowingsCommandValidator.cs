namespace LibraryMS.Application.Features.Borrowing.Commands.Create;

public sealed class CreateBorrowingsCommandValidator : AbstractValidator<CreateBorrowingsCommand>
{
    public CreateBorrowingsCommandValidator()
    {
        RuleFor(b => b.ClientId)
            .GreaterThan(0)
            .WithMessage("Client Id must be a valid ID greater than 0");

        RuleFor(b => b.CopyId)
            .GreaterThan(0)
            .WithMessage("Copy Id must be a valid ID greater than 0");
    }
}