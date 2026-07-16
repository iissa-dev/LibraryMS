using LibraryMS.Application.Common.Extensions;
using LibraryMS.Domain.Common.Specifications;

namespace LibraryMS.Application.Features.Reservations.Commands.Fulfill;

public sealed class FulfillReservationCommandHandler(IAppDbContext context)
    : IRequestHandler<FulfillReservationCommand, Result>
{
    public async Task<Result> Handle(FulfillReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await context.Reservations
            .SingleOrDefaultAsync(r => r.Id == request.ReserveId, cancellationToken);

        if (reservation is null)
            return Result.Failure("Reserve not found");

        if (!reservation.ReadyToBorrow)
        {
            return Result.Failure("Cannot complete the reservation. The reservation is not ready to borrow.");
        }

        var hasFine = await context.Fines
        .Specify(new HasUnpaidFinesSpecification(reservation.ClientId))
        .AnyAsync(cancellationToken);

        if (hasFine)
            return Result.Failure("Cannot fulfill reservation. Client has an unpaid fine and must pay it first.");

        var setting = await context.GetApplicationSettingsAsync(cancellationToken);

        // Borrow the Book

        var copy = await context.BookCopies
            .SingleOrDefaultAsync(c => c.Id == reservation.BookCopyId, cancellationToken);

        if (copy is null) return Result.Failure("Book copy associated with this reservation was not found.");

        var activeReservation = await context.Reservations
            .Specify(new HasActiveReservation(copy.Id))
            .SingleOrDefaultAsync(cancellationToken);

        if (activeReservation is not null && activeReservation.ClientId != request.ClientId)
            return Result.Failure("This copy is reserved for another client in the waiting list.");

        reservation.Fulfill(copy);

        var borrowingRecord = new BorrowingRecord
        {
            ClientId = reservation.ClientId,
            CopyId = copy.Id,
            BorrowingDate = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(setting.DefaultBorrowDays)
        };

        context.BorrowingRecords.Add(borrowingRecord);

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}
