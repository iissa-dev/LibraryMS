using LibraryMS.Application.Common.Extensions;
using LibraryMS.Application.DTOs.BookDtos;

namespace LibraryMS.Application.Features.BookCopies.Queries.GetAllCopies;

public sealed class GetAllCopiesQueryHandler(IAppDbContext context)
    : IRequestHandler<GetAllCopiesQuery, Result<PagedResult<ResponseBookCopiesDto>>>
{
    public async Task<Result<PagedResult<ResponseBookCopiesDto>>> Handle(GetAllCopiesQuery request, CancellationToken cancellationToken)
    {
        var query = context.BookCopies
            .AsNoTracking();

        if (request.BookId.HasValue)
        {
            query = query.Where(bc => bc.BookId == request.BookId);
        }

        if (request.FilterByStatus.HasValue)
        {
            query = query.Where(bc => bc.CopyStatus == (CopyStatus)request.FilterByStatus.Value);
        }

        var pagedResult = await query
        .OrderByDescending(bc => bc.CreatedOn)
        .ToPagedResultAsync(
            request.PageNumber,
            request.PageSize,
            selector: bc => new ResponseBookCopiesDto
            {
                BookCopyId = bc.Id,
                BookId = bc.BookId,
                Title = bc.Book.Title,
                Isbn = bc.Book.ISBN,
                SerialNumber = bc.SerialNumber,
                Status = bc.CopyStatus.ToString()
            },
            cancellationToken
        );

        return Result<PagedResult<ResponseBookCopiesDto>>.Success(pagedResult);
    }
}
