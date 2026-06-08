namespace LibraryMS.Application.Features.Reservations.Commands.Cancel;

public sealed class CancelCommandValidator : AbstractValidator<CancelCommand>
{
    public CancelCommandValidator()
    {
        RuleFor(r => r.ReserveId)
            .GreaterThan(0)
            .WithMessage("Reserve Id must be a valid ID greater than 0");
    }
}