using LibraryMS.Application.Common.Extensions;
using LibraryMS.Application.DTOs.ClientDto;
using LibraryMS.Application.DTOs.FineDto;

namespace LibraryMS.Application.Features.Fine.Queries.GetById;

public sealed class GetAllfinesByIdQueryHandler(IAppDbContext context)
    : IRequestHandler<GetAllFinesByIdQuery, Result<PagedResult<FineDetails>>>
{
    /// Get fine for some client
    public async Task<Result<PagedResult<FineDetails>>> Handle(GetAllFinesByIdQuery request, CancellationToken cancellationToken)
    {
        var query = context.Fines
            .AsNoTracking();

        if (request.ClientId.HasValue)
        {
            query = query.Where(f => f.ClientId == request.ClientId);
        }

        var pagedResult = await query
            .OrderByDescending(f => f.CreatedOn)
            .ToPagedResultAsync(
                request.PageNumber,
                request.PageSize,
                selector: f => new FineDetails
                {
                    FineId = f.Id,
                    BorrowingDate = f.BorrowingRecord.BorrowingDate,
                    ReturnDate = f.BorrowingRecord.ActualReturnDate ?? DateTime.UtcNow,
                    PaymentStatus = f.PaymentStatus.ToString(),
                    Reason = f.Reason,
                    FineAmount = f.FineAmount,
                    Borrower = new ClientSummaryDto
                    {
                        ClientId = f.ClientId,
                        LibraryCardNumber = f.Client.LibraryCardNumber,
                        ClientName = f.Client.Person.FirstName + " " + f.Client.Person.LastName
                    }
                },
                cancellationToken
            );

        return Result<PagedResult<FineDetails>>.Success(pagedResult);
    }
}
