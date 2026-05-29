using FluentValidation;

namespace LibraryMS.Application.Features.Client.Commands.RestoreClient;

public sealed class RestoreClientCommandValidator : AbstractValidator<RestoreClientCommand>
{
    public RestoreClientCommandValidator()
    {
        RuleFor(e => e.UserId).NotEmpty().WithMessage("User Id is required");
    }
}
