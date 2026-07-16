using LibraryMS.Application.DTOs.ReservationDto;

namespace LibraryMS.Application.Features.Reservations.Queries.GetById;

public sealed record GetAllClientReservationQuery(int PageNumber,
    int PageSize,
    int? ClientId,
    int? SearchByStatus) : IRequest<Result<PagedResult<ClientReservationDto>>>;
