using LibraryMS.Application.Common.Extensions;
using LibraryMS.Application.DTOs.BookDtos;
using LibraryMS.Application.DTOs.BorrowDto;
using LibraryMS.Application.DTOs.ClientDto;
namespace LibraryMS.Application.Features.Borrowing.Queries.GetFullBorrowDetailsById;

public sealed class GetFullBorrowDetailsQueryHandler(IAppDbContext context, IIdentityUser identityUser)
    : IRequestHandler<GetFullBorrowDetailsQuery, Result<PagedResult<BorrowDetails>>>
{
    public async Task<Result<PagedResult<BorrowDetails>>> Handle(GetFullBorrowDetailsQuery request, CancellationToken cancellationToken)
    {
        var client = await context.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == request.ClientId, cancellationToken);

        if (client is null)
            return Result<PagedResult<BorrowDetails>>.Failure($"Client with Id {request.ClientId} not found");

        var clientFullName = await identityUser.GetFullnameByIdAsync(client.Id);

        var query = context.BorrowingRecords
            .AsNoTracking()
            .Where(b => b.ClientId == request.ClientId)
            .OrderByDescending(b => b.CreatedOn);

        var pagedResult = await context.BorrowingRecords
            .ToPagedResultAsync(
                request.PageNumber,
                request.PageSize,
                selector: b => new BorrowDetails
                {
                    BorrowId = b.Id,
                    Book = new BookSummaryDto
                    {
                        BookId = b.BookCopy.BookId,
                        Title = b.BookCopy.Book.Title,
                        Author = b.BookCopy.Book.BookAuthors.Select(a => a.Author.FirstName + " " + a.Author.LastName).ToList()
                    },
                    Borrower = new ClientSummaryDto
                    {
                        ClientId = client.Id,
                        LibraryCardNumber = client.LibraryCardNumber,
                        ClientName = clientFullName ?? "Unknown"
                    },
                    BorrowDate = b.BorrowingDate,
                    DueDate = b.DueDate,
                    ReturnDate = b.ActualReturnDate,
                    Status = b.BookCopy.CopyStatus.ToString(),
                    FineAmount = b.Fine != null ? b.Fine.FineAmount : 0
                },
                cancellationToken
            );

        return Result<PagedResult<BorrowDetails>>.Success(pagedResult);
    }
}
