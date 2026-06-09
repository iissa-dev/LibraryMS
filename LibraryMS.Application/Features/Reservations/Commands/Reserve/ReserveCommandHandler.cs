using LibraryMS.Application.Common.Extensions;
using LibraryMS.Domain.Common.Specifications;

namespace LibraryMS.Application.Features.Reservations.Commands.Reserve;

public sealed class ReserveCommandHandler(IAppDbContext context)
    : IRequestHandler<ReserveCommand, Result>
{
    public async Task<Result> Handle(ReserveCommand request, CancellationToken cancellationToken)
    {
        var bookExisting = await context.Books.AnyAsync(b => b.Id == request.BookId, cancellationToken);
        if (!bookExisting) return Result.Failure($"Book with Id {request.BookId} not found");

        var clientExisting = await context.Clients.AnyAsync(c => c.Id == request.ClientId, cancellationToken);
        if (!clientExisting) return Result.Failure($"Client with Id {request.ClientId} not found");

        var isAlreadyReserved = await context.Reservations
            .AnyAsync(r => r.BookId == request.BookId
                        && r.ClientId == request.ClientId
                        && r.ReservationsStatus == ReservationsStatus.Waiting,
                    cancellationToken);

        if (isAlreadyReserved)
            return Result.Failure("You have already reserved this book and you are on the waiting list.");

        var isCurrentBorrowing = await context.BorrowingRecords
            .AnyAsync(b => b.ClientId == request.ClientId
                        && b.BookCopy.BookId == request.BookId
                        && b.ActualReturnDate == null,
                        cancellationToken);

        if (isCurrentBorrowing)
            return Result.Failure("You cannot reserve a book that you are currently borrowing. Please return it first.");

        var hasAvailableCopy = await context.BookCopies
            .AnyAsync(c => c.BookId == request.BookId
                        && c.CopyStatus == CopyStatus.Available, cancellationToken);

        var hasWaitingQueue = await context.Reservations
            .Specify(new HasWaitingQueueSpecification(request.BookId))
            .AnyAsync(cancellationToken);

        if (hasAvailableCopy && !hasWaitingQueue)
            return Result.Failure("There is an available copy currently on the shelf. You cannot reserve it, please borrow it directly.");

        var reservation = new Reservation
        {
            BookId = request.BookId,
            ClientId = request.ClientId,
            ReservationsStatus = ReservationsStatus.Waiting,
            ReservationDate = DateTime.UtcNow,

        };

        context.Reservations.Add(reservation);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
