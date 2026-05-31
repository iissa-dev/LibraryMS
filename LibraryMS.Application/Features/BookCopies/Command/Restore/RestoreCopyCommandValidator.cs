namespace LibraryMS.Application.Features.BookCopies.Command.Restore;

public sealed class RestoreCopyCommandValidator : AbstractValidator<RestoreCopyCommand>
{
    public RestoreCopyCommandValidator()
    {
        RuleFor(bc => bc.BookCopyId).NotEmpty().WithMessage("Copy Id is required");
    }
}