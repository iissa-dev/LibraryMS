using FluentValidation;

namespace LibraryMS.Application.Features.Person.Commands.UpdatePersonInfo;

public class UpdatePersonInfoCommandValidator : AbstractValidator<UpdatePersonInfoCommand>
{
    public UpdatePersonInfoCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty().WithMessage("User Id is required");
        RuleFor(c => c.FirstName).NotEmpty().WithMessage("First name is required");
        RuleFor(c => c.LastName).NotEmpty().WithMessage("Last name is required");
        RuleFor(c => c.Address).NotEmpty().WithMessage("Address is required");
        RuleFor(c => c.BirthDate).NotEmpty().WithMessage("BirthDate is required");
    }
}
