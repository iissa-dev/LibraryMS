using LibraryMS.Application.Common.Extensions;
using LibraryMS.Application.DTOs.BookDtos;

namespace LibraryMS.Application.Features.BookCopies.Queries.GetAllCopies;

public sealed class GetAllCopiesQueryHandler(IAppDbContext context)
    : IRequestHandler<GetAllCopiesQuery, Result<PagedResult<ResponseBookCopiesDto>>>
{
    public async Task<Result<PagedResult<ResponseBookCopiesDto>>> Handle(GetAllCopiesQuery request, CancellationToken cancellationToken)
    {
        var query = context.BookCopies
            .AsNoTracking()
            .OrderByDescending(bc => bc.CreatedOn);
        
        var pagedResult = await context.BookCopies
        .ToPagedResultAsync(
            request.PageNumber,
            request.PageSize,
            selector: bc => new ResponseBookCopiesDto
            {
                BookCopyId = bc.Id,
                BookId = bc.BookId,
                Title = bc.Book.Title,
                Isbn = bc.Book.ISBN,
                IsAvailable = bc.IsAvailable,
                SerialNumber = bc.SerialNumber,
                Status = bc.CopyStatus.ToString()
            },
            cancellationToken
        );

        return Result<PagedResult<ResponseBookCopiesDto>>.Success(pagedResult);
    }
}
