namespace LibraryMS.Application.Features.BookCopies.Command.UpdateStatus;

public sealed class UpdateStatusCopyCommandValidator : AbstractValidator<UpdateStatusCopyCommand>
{
    public UpdateStatusCopyCommandValidator()
    {
        RuleFor(bc => bc.BookCopyId)
        .NotEmpty()
        .WithMessage("Book copy id is required");

        RuleFor(bc => (CopyStatus)bc.CopyStatus)
        .IsInEnum()
        .WithMessage("Invalid status number");
    }
}