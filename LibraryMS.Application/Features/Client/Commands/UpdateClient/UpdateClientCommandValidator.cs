namespace LibraryMS.Application.Features.Client.Commands.UpdateClient;

public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
{
    public UpdateClientCommandValidator()
    {
        RuleFor(c => c.LibraryCardNumber).NotEmpty().WithMessage("Library card number is required");
    }
}