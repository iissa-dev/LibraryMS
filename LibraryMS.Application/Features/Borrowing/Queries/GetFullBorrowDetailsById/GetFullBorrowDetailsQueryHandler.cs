using LibraryMS.Application.Common.Extensions;
using LibraryMS.Application.DTOs.BookDtos;
using LibraryMS.Application.DTOs.BorrowDto;
using LibraryMS.Application.DTOs.ClientDto;
namespace LibraryMS.Application.Features.Borrowing.Queries.GetFullBorrowDetailsById;

public sealed class GetFullBorrowDetailsQueryHandler(IAppDbContext context)
    : IRequestHandler<GetFullBorrowDetailsQuery, Result<PagedResult<BorrowDetails>>>
{
    public async Task<Result<PagedResult<BorrowDetails>>> Handle(GetFullBorrowDetailsQuery request, CancellationToken cancellationToken)
    {
        var query = context.BorrowingRecords
            .AsNoTracking();

        if (request.ClientId.HasValue)
        {
            query = query.Where(b => b.ClientId == request.ClientId);
        }

        var pagedResult = await query
            .IgnoreQueryFilters()
            .OrderByDescending(b => b.CreatedOn)
            .ThenByDescending(b => b.Id)
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
                        Author = new List<string>() // don't fetch it here to prevent duplicate
                    },
                    Borrower = new ClientSummaryDto
                    {
                        ClientId = b.ClientId,
                        LibraryCardNumber = b.Client.LibraryCardNumber,
                        ClientName = b.Client.Person != null
                            ? b.Client.Person.FirstName + " " + b.Client.Person.LastName
                            : "Unknown"
                    },
                    CopyId = b.CopyId,
                    BorrowDate = b.BorrowingDate,
                    DueDate = b.DueDate,
                    ReturnDate = b.ActualReturnDate,
                    Status = b.BookCopy.CopyStatus.ToString(),
                    FineAmount = b.Fine != null ? b.Fine.FineAmount : 0
                },
                cancellationToken
            );

        // Get the authors
        if (pagedResult.Items != null && pagedResult.Items.Any())
        {
            var bookIds = pagedResult.Items
                .Select(d => d.Book.BookId)
                .Distinct()
                .ToList();

            var bookAuthorsMap = await context.BookAuthors
                .AsNoTracking()
                .Where(ba => bookIds.Contains(ba.BookId))
                .Select(ba => new { ba.BookId, AuthorName = ba.Author.FirstName + " " + ba.Author.LastName })
                .ToListAsync(cancellationToken);

            foreach (var item in pagedResult.Items)
            {
                item.Book.Author = bookAuthorsMap
                    .Where(ba => ba.BookId == item.Book.BookId)
                    .Select(ba => ba.AuthorName);
            }
        }

        return Result<PagedResult<BorrowDetails>>.Success(pagedResult);
    }
}
