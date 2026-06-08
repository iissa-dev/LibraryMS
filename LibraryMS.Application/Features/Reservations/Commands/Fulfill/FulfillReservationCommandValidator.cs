namespace LibraryMS.Application.Features.Reservations.Commands.Fulfill;

public sealed class FulfillReservationCommandValidator : AbstractValidator<FulfillReservationCommand>
{
    public FulfillReservationCommandValidator()
    {
        RuleFor(r => r.ReserveId)
            .GreaterThan(0)
            .WithMessage("Reserve Id must be valid ID and greater than zero");
    }
}