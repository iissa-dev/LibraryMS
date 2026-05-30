using FluentValidation;

namespace LibraryMS.Application.Features.Author.Commands.Delete;

public sealed class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(x => x.Id)
        .GreaterThan(0)
        .WithMessage("Author Id must be greater than 0.")
        .NotEmpty()
        .WithMessage("Author Id is required.");
    }
}