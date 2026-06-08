namespace LibraryMS.Application.Features.Reservations.Commands.Cancel;

public sealed record CancelCommand(int ReserveId) : IRequest<Result>;
