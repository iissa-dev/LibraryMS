using LibraryMS.Domain.Common.Events;

namespace LibraryMS.Application.Features.Borrowing.Commands.Create;

public sealed class CreateBorrowingsCommandHandler(IAppDbContext context, IPublisher publisher)
    : IRequestHandler<CreateBorrowingsCommand, Result>
{
    public async Task<Result> Handle(CreateBorrowingsCommand request, CancellationToken cancellationToken)
    {
        if (!await context.Clients.AnyAsync(c => c.Id == request.ClientId, cancellationToken))
            return Result.Failure("Client not found");

        var setting = await context.Settings
        .FirstOrDefaultAsync(cancellationToken);
        if (setting is null) return Result.Failure("Settings not found");

        var copy = await context.BookCopies.FindAsync(request.CopyId, cancellationToken);
        if (copy is null) return Result.Failure("Copy not found");

        if (!copy.IsAvailable) return Result.Failure("Copy not available");

        var borrowing = new BorrowingRecord
        {
            CopyId = request.CopyId,
            ClientId = request.ClientId,
            DueDate = DateTime.UtcNow.AddDays(setting.DefaultBorrowDays),
        };

        copy.UpdateStatus(CopyStatus.Borrowed);

        context.BorrowingRecords.Add(borrowing);
        await context.SaveChangesAsync(cancellationToken);

        var borrowedEvent = new BookBorrowedEvent(borrowing.CopyId, borrowing.ClientId, borrowing.DueDate);
        await publisher.Publish(borrowedEvent, cancellationToken);

        return Result.Success;
    }
}
