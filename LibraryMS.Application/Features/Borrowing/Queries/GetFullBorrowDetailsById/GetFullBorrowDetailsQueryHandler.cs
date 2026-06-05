using LibraryMS.Application.DTOs.BookDtos;
using LibraryMS.Application.DTOs.BorrowDto;
namespace LibraryMS.Application.Features.Borrowing.Queries.GetFullBorrowDetailsById;

public sealed class GetFullBorrowDetailsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetFullBorrowDetailsQuery, Result<PagedResult<BorrowDetails>>>
{
    public async Task<Result<PagedResult<BorrowDetails>>> Handle(GetFullBorrowDetailsQuery request, CancellationToken cancellationToken)
    {
        var clientInfo = await unitOfWork.Clients.GetClientWithUserInfoByClientIdAsync(request.ClientId);
        if(clientInfo is null) 
            return Result<PagedResult<BorrowDetails>>.Failure($"Client with Id {request.ClientId} not found");

        var (items, totalCount) = await unitOfWork.Repository<BorrowingRecord>()
        .GetPagedProjectedAsync(
            selector: b => new BorrowDetails
            {
                BorrowId = b.Id,
                Book = new BookSummaryDto
                {
                    BookId = b.BookCopy.BookId,
                    Title = b.BookCopy.Book.Title,
                    Author = b.BookCopy.Book.BookAuthors.Select(a => a.Author.FirstName + " " + a.Author.LastName).ToList()
                },
                Borrower = new DTOs.UserDto.UserSummaryDto
                {
                    UserId = clientInfo.UserId,
                    LibraryCardNumber = clientInfo.LibraryCardNumber,
                    ClientName = $"{clientInfo.FirstName} {clientInfo.LastName}"
                },
                BorrowDate = b.BorrowingDate,
                DueDate = b.DueDate,
                ReturnDate = b.ActualReturnDate,
                Status = b.BookCopy.CopyStatus.ToString(),
                FineAmount = b.Fine != null ? b.Fine.FineAmount : 0
            },
            request.PageNumber,
            request.PageSize,
            predicate: b => b.ClientId == request.ClientId,
            orderBy: b => b.OrderByDescending(b => b.CreatedOn),
            true,
            cancellationToken
        );

        var page = new PagedResult<BorrowDetails>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize)
        };

        return Result<PagedResult<BorrowDetails>>.Success(page);
    }
}
