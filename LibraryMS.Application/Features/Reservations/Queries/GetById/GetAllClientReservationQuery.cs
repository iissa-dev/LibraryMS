using LibraryMS.Application.DTOs.ReservationDto;

namespace LibraryMS.Application.Features.Reservations.Queries.GetById;

public sealed record GetAllClientReservationQuery(int ClientId) : IRequest<Result<List<ClientReservationDto>>>;

public sealed class GetAllClientReservationQueryHandler(IAppDbContext context)
    : IRequestHandler<GetAllClientReservationQuery, Result<List<ClientReservationDto>>>
{
    public async Task<Result<List<ClientReservationDto>>> Handle(GetAllClientReservationQuery request, CancellationToken cancellationToken)
    {
        var clientReservations = await context.Reservations
            .Where(r => r.ClientId == request.ClientId)
            .OrderByDescending(r => r.ReservationDate)
            .Select(r => new ClientReservationDto
            {
                ReservationId = r.Id,
                BookId = r.BookId,
                BookTitle = r.Book.Title,
                AuthorName = r.Book.BookAuthors.Select(a => $"{a.Author.FirstName} {a.Author.LastName}"),
                ReservationDate = r.ReservationDate,
                StatusName = r.ReservationsStatus.ToString(),
                BookCopyId = r.BookCopyId,
                QueuePosition = r.ReservationsStatus == ReservationsStatus.Waiting
                    ? context.Reservations.Count(x => x.BookId == r.BookId
                                                    && x.ReservationsStatus == ReservationsStatus.Waiting
                                                    && x.ReservationDate <= r.ReservationDate)
                    : 0
            })
            .ToListAsync(cancellationToken);

        return Result<List<ClientReservationDto>>.Success(clientReservations);
    }
}

public sealed class GetAllClientReservationQueryValidator : AbstractValidator<GetAllClientReservationQuery>
{
    public GetAllClientReservationQueryValidator()
    {
        RuleFor(r => r.ClientId)
            .GreaterThan(0)
            .WithMessage("Client Id must be valid Id and Greater than zero");
    }
}