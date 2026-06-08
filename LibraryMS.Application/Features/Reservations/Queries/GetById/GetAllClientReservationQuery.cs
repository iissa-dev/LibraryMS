using LibraryMS.Application.DTOs.ReservationDto;

namespace LibraryMS.Application.Features.Reservations.Queries.GetById;

public sealed record GetAllClientReservationQuery(int ClientId) : IRequest<Result<List<ClientReservationDto>>>;
