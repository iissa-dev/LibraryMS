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

        if (reservation.ReservationsStatus != ReservationsStatus.ReadyForPickup &&
            reservation.ReservationsStatus != ReservationsStatus.Notified)
        {
            return Result.Failure("Cannt complete the reserve the reserve not ready to borrow");
        }

        var hasFine = await context.Fines
        .AnyAsync(f => f.ClientId == reservation.ClientId
                    && f.PaymentStatus == PaymentStatus.Unpaid, cancellationToken);

        if (hasFine)
            return Result.Failure("Cannot fulfill reservation. Client has an unpaid fine and must pay it first.");

        var setting = await context.Settings
        .AsNoTracking()
        .FirstOrDefaultAsync(cancellationToken);

        if (setting is null) return Result.Failure("No settings");

        reservation.ReservationsStatus = ReservationsStatus.Completed;
        context.Reservations.Update(reservation);

        // Borrow the Book

        var copy = await context.BookCopies
            .SingleOrDefaultAsync(c => c.Id == reservation.BookCopyId, cancellationToken);
        if (copy is not null)
        {
            copy.CopyStatus = CopyStatus.Borrowed;
            context.BookCopies.Update(copy);

            var borrowingRecord = new BorrowingRecord
            {
                ClientId = reservation.ClientId,
                CopyId = copy.Id,
                BorrowingDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(setting.DefaultBorrowDays)
            };

            context.BorrowingRecords.Add(borrowingRecord);
        }

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}
