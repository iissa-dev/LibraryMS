using FluentValidation;

namespace LibraryMS.Application.Features.Auth.Queries.User;

public class CurrentUserQueryValidator: AbstractValidator<CurrentUserQuery>
{
    public CurrentUserQueryValidator()
    {
        RuleFor(u => u.UserId).NotEmpty().WithMessage("User Id is reqiured");
    }
}
