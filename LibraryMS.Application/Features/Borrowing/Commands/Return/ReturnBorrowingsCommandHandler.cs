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

        var setting = await context.Settings
            .FirstOrDefaultAsync(cancellationToken);
        if (setting is null) return Result.Failure("Settings not found");

        borrowing.BookCopy?.MakeStatusAvailable();

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
