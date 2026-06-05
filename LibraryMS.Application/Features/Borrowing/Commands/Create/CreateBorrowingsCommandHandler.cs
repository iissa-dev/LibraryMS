using LibraryMS.Domain.Common.Events;

namespace LibraryMS.Application.Features.Borrowing.Commands.Create;

public sealed class CreateBorrowingsCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
    : IRequestHandler<CreateBorrowingsCommand, Result>
{
    public async Task<Result> Handle(CreateBorrowingsCommand request, CancellationToken cancellationToken)
    {
        if (!await unitOfWork.Clients.ExistsAsync(c => c.Id == request.ClientId))
            return Result.Failure("Client not found");

        var setting = await unitOfWork
        .Repository<Setting>()
        .AsQueryable()
        .FirstOrDefaultAsync(cancellationToken);
        if (setting is null) return Result.Failure("Settings not found");

        var copy = await unitOfWork.BookCopies.GetByIdAsync(request.CopyId);
        if (copy is null) return Result.Failure("Copy not found");

        if (!copy.IsAvailable) return Result.Failure("Copy not available");

        var borrowing = new BorrowingRecord
        {
            CopyId = request.CopyId,
            ClientId = request.ClientId,
            DueDate = DateTime.UtcNow.AddDays(setting.DefaultBorrowDays),
        };

        copy.UpdateStatus(CopyStatus.Borrowed);

        unitOfWork.Repository<BorrowingRecord>().Add(borrowing);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var borrowedEvent = new BookBorrowedEvent(borrowing.CopyId, borrowing.ClientId, borrowing.DueDate);
        await mediator.Publish(borrowedEvent, cancellationToken);

        return Result.Success;
    }
}
