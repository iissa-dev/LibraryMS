using FluentValidation;

namespace LibraryMS.Application.Features.Auth.Queries.User;

public class CurrentUserCommandValidator: AbstractValidator<CurrentUserCommand>
{
    public CurrentUserCommandValidator()
    {
        RuleFor(u => u.UserId).NotEmpty().WithMessage("User Id is reqiured");
    }
}
