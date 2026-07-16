namespace LibraryMS.Application.Features.Reservations.Commands.Fulfill;

public sealed class FulfillReservationCommandValidator : AbstractValidator<FulfillReservationCommand>
{
    public FulfillReservationCommandValidator()
    {
        RuleFor(r => r.ReserveId)
            .GreaterThan(0)
            .WithMessage("Reserve Id must be valid ID and greater than zero");

        RuleFor(r => r.ClientId)
            .GreaterThan(0)
            .WithMessage("Client Id must be valid ID and greater than zero");
    }
}