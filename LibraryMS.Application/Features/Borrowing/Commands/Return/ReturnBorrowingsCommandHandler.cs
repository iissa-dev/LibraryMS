using LibraryMS.Application.Common.Extensions;
using LibraryMS.Domain.Common.Specifications;

namespace LibraryMS.Application.Features.Borrowing.Commands.Return;

public sealed class ReturnBorrowingsCommandHandler(IAppDbContext context)
    : IRequestHandler<ReturnBorrowingsCommand, Result>
{
    public async Task<Result> Handle(ReturnBorrowingsCommand request, CancellationToken cancellationToken)
    {
        var borrowing = await context.BorrowingRecords
        .Include(b => b.BookCopy)
        .FirstOrDefaultAsync(b => b.Id == request.BorrowingId, cancellationToken);

        if (borrowing is null) return Result.Failure("Borrowing not found");

        if (borrowing.ActualReturnDate is not null)
            return Result.Failure("This book has already been returned");

        if (borrowing.BookCopy is null) return Result.Failure("Copy not found");

        var setting = await context.GetApplicationSettingsAsync(cancellationToken);

        var nextReservation = await context.Reservations
            .Specify(new HasWaitingQueueSpecification(borrowing.BookCopy.BookId))
            .OrderBy(r => r.ReservationDate)
            .FirstOrDefaultAsync(cancellationToken);

        if (nextReservation is not null)
        {
            nextReservation.ReservationsStatus = ReservationsStatus.ReadyForPickup;
            nextReservation.BookCopyId = borrowing.CopyId;
            context.Reservations.Update(nextReservation);

            borrowing.BookCopy.CopyStatus = CopyStatus.Reserved; // for the next reserver
        }
        else
        {
            // no reservers
            borrowing.BookCopy.MakeStatusAvailable();
        }

        if (borrowing.IsLate)
        {
            var lateDays = borrowing.LateDate;
            borrowing.AddFine(new Domain.Entities.Fine
            {
                BorrowingRecordId = borrowing.Id,
                ClientId = borrowing.ClientId,
                NumberOfLateDays = lateDays,
                Reason = $"Return late by {lateDays} days.",
                FineAmount = setting.DefaultFinePerDay * lateDays
            });
        }

        borrowing.MarkAsReturned();
        context.BorrowingRecords.Update(borrowing);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
