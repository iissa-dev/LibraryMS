using LibraryMS.Application.Common.Extensions;
using LibraryMS.Domain.Common.Events;
using LibraryMS.Domain.Common.Specifications;

namespace LibraryMS.Application.Features.Borrowing.Commands.Create;

public sealed class CreateBorrowingsCommandHandler(IAppDbContext context, IPublisher publisher)
    : IRequestHandler<CreateBorrowingsCommand, Result>
{
    public async Task<Result> Handle(CreateBorrowingsCommand request, CancellationToken cancellationToken)
    {
        if (!await context.Clients.AnyAsync(c => c.Id == request.ClientId, cancellationToken))
            return Result.Failure("Client not found");

        var setting = await context.GetApplicationSettingsAsync(cancellationToken);

        var copy = await context.BookCopies.FindAsync(request.CopyId, cancellationToken);
        if (copy is null) return Result.Failure("Copy not found");

        if (copy.CopyStatus == CopyStatus.Borrowed) return Result.Failure("This copy is already borrowed");

        var hasFine = await context.Fines
            .Specify(new HasUnpaidFinesSpecification(request.ClientId))
            .AnyAsync(cancellationToken);

        if (hasFine)
            return Result.Failure("Cannot borrow. Client has an unpaid fine and must pay it first.");

        if (copy.CopyStatus == CopyStatus.Reserved)
        {
            var activeReservation = await context.Reservations
                .FirstOrDefaultAsync(r => r.BookCopyId == copy.Id &&
                    (r.ReservationsStatus == ReservationsStatus.ReadyForPickup ||
                    r.ReservationsStatus == ReservationsStatus.Notified),
                    cancellationToken);
            if (activeReservation is not null)
            {
                if (activeReservation.ClientId != request.ClientId)
                {
                    return Result.Failure("This copy is reserved for another client in the waiting list.");
                }

                activeReservation.ReservationsStatus = ReservationsStatus.Completed;
                context.Reservations.Update(activeReservation);
            }
        }
        else if (copy.IsAvailable)
        {
            var hasWaitingQueue = await context.Reservations
                .Specify(new HasWaitingQueueSpecification(copy.BookId))
                .AnyAsync(cancellationToken);

            if (hasWaitingQueue)
                return Result.Failure("Cannot borrow directly; there are clients on the waiting list for this book. Please place a reservation.");
        }

        var borrowing = new BorrowingRecord
        {
            CopyId = request.CopyId,
            ClientId = request.ClientId,
            DueDate = DateTime.UtcNow.AddDays(setting.DefaultBorrowDays),
        };

        copy.UpdateStatus(CopyStatus.Borrowed);

        context.BorrowingRecords.Add(borrowing);
        await context.SaveChangesAsync(cancellationToken);

        var borrowedEvent = new BookBorrowedEvent(request.CopyId, request.ClientId, borrowing.DueDate);
        await publisher.Publish(borrowedEvent, cancellationToken);

        return Result.Success;
    }
}
