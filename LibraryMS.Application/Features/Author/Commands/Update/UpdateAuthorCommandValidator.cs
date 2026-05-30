using FluentValidation;

namespace LibraryMS.Application.Features.Author.Commands.Update;

public sealed class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(x => x.Id)
        .GreaterThan(0)
        .WithMessage("Id must be greater than 0.");

        RuleFor(x => x.FirstName)
        .NotEmpty()
        .WithMessage("First name is required.")
        .MaximumLength(50)
        .WithMessage("First name must not exceed 50 characters.");

        RuleFor(x => x.LastName)
        .NotEmpty()
        .WithMessage("Last name is required.")
        .MaximumLength(50)
        .WithMessage("Last name must not exceed 50 characters.");

        RuleFor(x => x.Biography)
        .MaximumLength(250)
        .WithMessage("Biography must not exceed 250 characters.");
    }
}