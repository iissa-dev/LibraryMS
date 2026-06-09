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
            return Result.Failure("Cannt complete the reserve the reserve not ready to borrow");
        }

        var hasFine = await context.Fines
        .Specify(new HasUnpaidFinesSpecification(reservation.ClientId))
        .AnyAsync(cancellationToken);

        if (hasFine)
            return Result.Failure("Cannot fulfill reservation. Client has an unpaid fine and must pay it first.");

        var setting = await context.GetApplicationSettingsAsync(cancellationToken);

        reservation.ReservationsStatus = ReservationsStatus.Completed;
        context.Reservations.Update(reservation);

        // Borrow the Book

        var copy = await context.BookCopies
            .SingleOrDefaultAsync(c => c.Id == reservation.BookCopyId, cancellationToken);
        if (copy is not null)
        {
            reservation.Fulfill(copy);

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
