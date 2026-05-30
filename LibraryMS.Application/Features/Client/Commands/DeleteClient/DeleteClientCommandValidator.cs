namespace LibraryMS.Application.Features.Client.Commands.DeleteClient;

public sealed class DeleteClientCommandValidator : AbstractValidator<DeleteClientCommand>
{
    public DeleteClientCommandValidator()
    {
        RuleFor(e => e.UserId).NotEmpty().WithMessage("User Id is required");
    }
}
