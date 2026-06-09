using LibraryMS.Application.Common.Extensions;
using LibraryMS.Domain.Common.Specifications;

namespace LibraryMS.Application.Features.Reservations.Commands.Cancel;

public sealed class CancelCommandHandler(IAppDbContext context)
    : IRequestHandler<CancelCommand, Result>
{
    public async Task<Result> Handle(CancelCommand request, CancellationToken cancellationToken)
    {
        var reserveExisting = await context.Reservations
            .SingleOrDefaultAsync(r => r.Id == request.ReserveId, cancellationToken);

        if (reserveExisting is null)
            return Result.Failure($"Reservation with Id {request.ReserveId} not foune");

        var previousStatus = reserveExisting.ReservationsStatus;

        reserveExisting.CancelReservation(); // update status
        context.Reservations.Update(reserveExisting);

        // check if the reserve happen
        if (reserveExisting.ReadyToBorrow)
        {
            // check for any waiting reserve for the same book
            var nextReservation = await context.Reservations
                .Specify(new HasWaitingQueueSpecification(reserveExisting.BookId))
                .OrderBy(r => r.ReservationDate)
                .FirstOrDefaultAsync(cancellationToken);

            if (nextReservation is not null)
            {
                // give to next one
                nextReservation.ReservationsStatus = ReservationsStatus.ReadyForPickup;
                nextReservation.BookCopyId = reserveExisting.BookCopyId; // new reserve 
                context.Reservations.Update(nextReservation);
            }
            else
            {
                // no client wait make it available
                var bookCopy = await context.BookCopies
                    .SingleOrDefaultAsync(c => c.Id == reserveExisting.BookCopyId, cancellationToken);

                if (bookCopy is not null)
                {
                    bookCopy.CopyStatus = CopyStatus.Available;
                    context.BookCopies.Update(bookCopy);
                }
            }
        }

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
