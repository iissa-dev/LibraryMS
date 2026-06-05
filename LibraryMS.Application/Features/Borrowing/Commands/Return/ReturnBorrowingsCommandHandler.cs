namespace LibraryMS.Application.Features.Borrowing.Commands.Return;

public sealed class ReturnBorrowingsCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ReturnBorrowingsCommand, Result>
{
    public async Task<Result> Handle(ReturnBorrowingsCommand request, CancellationToken cancellationToken)
    {
        var borrowing = await unitOfWork.Repository<BorrowingRecord>()
        .AsQueryable()
        .Include(b => b.BookCopy)
        .FirstOrDefaultAsync(b => b.Id == request.BorrowingId, cancellationToken);

        if (borrowing is null) return Result.Failure("Borrowing not found");

        if (borrowing.ActualReturnDate is not null)
            return Result.Failure("This book has already been returned");

        var setting = await unitOfWork
        .Repository<Setting>()
        .AsQueryable()
        .FirstOrDefaultAsync(cancellationToken);
        if (setting is null) return Result.Failure("Settings not found");

        borrowing.BookCopy?.MakeStatusAvailable();

        if (borrowing.IsLate)
        {
            var lateDays = borrowing.LateDate;
            borrowing.AddFine(new Fine
            {
                BorrowingRecordId = borrowing.Id,
                ClientId = borrowing.ClientId,
                NumberOfLateDays = lateDays,
                Reason = $"Return late by {lateDays} days.",
                FineAmount = setting.DefaultFinePerDay * lateDays
            });
        }

        borrowing.MarkAsReturned();
        unitOfWork.Repository<BorrowingRecord>().Update(borrowing);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
