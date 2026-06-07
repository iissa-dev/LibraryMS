using LibraryMS.Application.Common.Extensions;
using LibraryMS.Application.DTOs.ClientDto;
using LibraryMS.Application.DTOs.FineDto;

namespace LibraryMS.Application.Features.Fine.Queries.GetById;

public sealed class GetAllfinesByIdQueryHandler(IAppDbContext context)
    : IRequestHandler<GetAllfinesByIdQuery, Result<PagedResult<FineDetails>>>
{
    /// Get fine for some client
    public async Task<Result<PagedResult<FineDetails>>> Handle(GetAllfinesByIdQuery request, CancellationToken cancellationToken)
    {
        var clientInfo = await context.Clients
        .AsNoTracking()
        .Include(c => c.Person)
        .SingleOrDefaultAsync(c => c.Id == request.ClientId, cancellationToken);

        if (clientInfo is null)
            return Result<PagedResult<FineDetails>>.Failure($"Client with Id {request.ClientId} not found");

        var query = context.Fines
            .AsNoTracking()
            .Where(f => f.ClientId == request.ClientId)
            .OrderByDescending(f => f.CreatedOn);

        var pagedResult = await query
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
                        ClientId = clientInfo.Id,
                        LibraryCardNumber = clientInfo.LibraryCardNumber,
                        ClientName = $"{clientInfo.Person.FirstName} {clientInfo.Person.LastName}"
                    }
                },
                cancellationToken
            );

        return Result<PagedResult<FineDetails>>.Success(pagedResult);
    }
}
