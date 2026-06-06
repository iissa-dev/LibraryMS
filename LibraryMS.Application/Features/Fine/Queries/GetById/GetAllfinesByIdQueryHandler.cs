using LibraryMS.Application.DTOs.FineDto;
using LibraryMS.Application.DTOs.UserDto;

namespace LibraryMS.Application.Features.Fine.Queries.GetById;

public sealed class GetAllfinesByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllfinesByIdQuery, Result<PagedResult<FineDetails>>>
{
    public async Task<Result<PagedResult<FineDetails>>> Handle(GetAllfinesByIdQuery request, CancellationToken cancellationToken)
    {
        // var clientInfo = await unitOfWork.Clients.GetClientWithUserInfoByClientIdAsync(request.ClientId);
        // if (clientInfo is null)
        //     return Result<PagedResult<FineDetails>>.Failure($"Client with Id {request.ClientId} not found");

        // var (items, totalCount) = await unitOfWork.Repository<Domain.Entities.Fine>()
        // .GetPagedProjectedAsync(
        //     selector: f => new FineDetails
        //     {
        //         FineId = f.Id,
        //         BorrowingDate = f.BorrowingRecord.BorrowingDate,
        //         ReturnDate = f.BorrowingRecord.ActualReturnDate ?? DateTime.UtcNow,
        //         PaymentStatus = f.PaymentStatus.ToString(),
        //         Reason = f.Reason,
        //         FineAmount = f.FineAmount,
        //         Borrower = new UserSummaryDto
        //         {
        //             UserId = clientInfo.UserId,
        //             LibraryCardNumber = clientInfo.LibraryCardNumber,
        //             ClientName = $"{clientInfo.FirstName} {clientInfo.LastName}"
        //         }
        //     },
        //     request.PageNumber,
        //     request.PageSize,
        //     predicate: f => f.ClientId == request.ClientId,
        //     orderBy: f => f.OrderByDescending(f => f.CreatedOn),
        //     ignoreQueryFilters: false,
        //     cancellationToken
        // );

        // var page = new PagedResult<FineDetails>
        // {
        //     Items = items,
        //     TotalCount = totalCount,
        //     PageNumber = request.PageNumber,
        //     PageSize = request.PageSize,
        //     TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize)
        // };

        return Result<PagedResult<FineDetails>>.Success(new PagedResult<FineDetails>());
    }
}
