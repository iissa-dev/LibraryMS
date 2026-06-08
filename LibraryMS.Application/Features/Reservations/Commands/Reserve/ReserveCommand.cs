namespace LibraryMS.Application.Features.Reservations.Commands.Reserve;

public sealed record ReserveCommand(int BookId, int ClientId) : IRequest<Result>;
