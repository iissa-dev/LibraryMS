using LibraryMS.Application.Common.Extensions;
using LibraryMS.Application.DTOs.ReservationDto;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryMS.Application.Features.Reservations.Queries.GetById;

public sealed class GetAllClientReservationQueryHandler(IAppDbContext context)
    : IRequestHandler<GetAllClientReservationQuery, Result<PagedResult<ClientReservationDto>>>
{
    public async Task<Result<PagedResult<ClientReservationDto>>> Handle(GetAllClientReservationQuery request, CancellationToken cancellationToken)
    {
        var query = context.Reservations
            .AsNoTracking()
            .IgnoreQueryFilters();

        if (request.ClientId.HasValue)
        {
            query = query.Where(r => r.ClientId == request.ClientId);
        }

        if (request.SearchByStatus.HasValue)
        {
            query = query.Where(r => r.ReservationsStatus == (ReservationsStatus)request.SearchByStatus);
        }

        var clientReservations = await query
            .OrderByDescending(r => r.ReservationDate)
            .ToPagedResultAsync(
                request.PageNumber,
                request.PageSize,
                selector: r => new ClientReservationDto
                {
                    ReservationId = r.Id,
                    BookId = r.BookId,
                    BookTitle = r.Book.Title,
                    ReservationDate = r.ReservationDate,
                    StatusName = r.ReservationsStatus.ToString(),
                    BookCopyId = r.BookCopyId,
                    QueuePosition = r.ReservationsStatus == ReservationsStatus.Waiting
                    ? context.Reservations.Count(x => x.BookId == r.BookId
                                                    && x.ReservationsStatus == ReservationsStatus.Waiting
                                                    && x.ReservationDate <= r.ReservationDate)
                    : 0
                },
                cancellationToken
            );

        return Result<PagedResult<ClientReservationDto>>.Success(clientReservations);
    }
}
