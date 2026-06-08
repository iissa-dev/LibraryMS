using LibraryMS.Application.DTOs.FineDto;

namespace LibraryMS.Application.Features.Fine.Queries.CheckFineStatus;

public sealed class CheckFineStatusQueryHandler(IAppDbContext context)
    : IRequestHandler<CheckFineStatusQuery, Result<FineStatusDto>>
{
    public async Task<Result<FineStatusDto>> Handle(CheckFineStatusQuery request, CancellationToken cancellationToken)
    {
        var borrowing = await context.BorrowingRecords
            .Include(b => b.Fine)
            .FirstOrDefaultAsync(b => b.Id == request.BorrowingId, cancellationToken);

        if (borrowing is null)
            return Result<FineStatusDto>.Failure("Borrowing record not found");

        // Already return it and there is fine
        if (borrowing.ActualReturnDate is not null && borrowing.Fine is not null)
        {
            return Result<FineStatusDto>.Success(new FineStatusDto
            {
                FineId = borrowing.Fine.Id,
                FineAmount = borrowing.Fine.FineAmount,
                IsPaid = borrowing.Fine.PaymentStatus == PaymentStatus.Paid,
                LateDays = borrowing.Fine.NumberOfLateDays,
                Status = borrowing.Fine.PaymentStatus.ToString()
            });
        }

        var setting = await context.Settings.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        if (setting is null) return Result<FineStatusDto>.Failure("No settings");

        // Not return and late
        if (borrowing.ActualReturnDate is null && borrowing.DueDate < DateTime.UtcNow)
        {
            var liveLateDays = (DateTime.UtcNow - borrowing.DueDate).Days;
            var liveAmount = liveLateDays * setting.DefaultFinePerDay;

            return Result<FineStatusDto>.Success(new FineStatusDto
            {
                FineId = 0,
                FineAmount = liveAmount,
                IsPaid = false,
                LateDays = liveLateDays,
                Status = PaymentStatus.Unpaid.ToString()
            });
        }

        // Has no fine
        return Result<FineStatusDto>.Success(new FineStatusDto { FineId = 0, FineAmount = 0, IsPaid = false, LateDays = 0, Status = "No Fine" });
    }
}
