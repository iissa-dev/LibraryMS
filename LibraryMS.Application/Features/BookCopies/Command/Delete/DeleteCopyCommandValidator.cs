
namespace LibraryMS.Application.Features.BookCopies.Command.Delete;

public sealed class DeleteCopyCommandValidator : AbstractValidator<DeleteCopyCommand>
{
    public DeleteCopyCommandValidator()
    {
        RuleFor(bc => bc.BookCopyId).NotEmpty().WithMessage("Copy Id is required");
    }
}