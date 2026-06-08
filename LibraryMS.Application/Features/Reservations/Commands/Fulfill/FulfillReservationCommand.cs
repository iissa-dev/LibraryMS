namespace LibraryMS.Application.Features.Reservations.Commands.Fulfill;

public sealed record FulfillReservationCommand(int ReserveId) : IRequest<Result>;
