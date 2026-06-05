using LibraryMS.Application.Common.Extensions;
using LibraryMS.Application.DTOs.AuthorDto;
using LibraryMS.Application.DTOs.BookDtos;

namespace LibraryMS.Application.Features.Book.Queries.GetAllBook;

public class GetAllBooksQueryHandler(IAppDbContext context)
    : IRequestHandler<GetAllBooksQuery, Result<PagedResult<ResponseBookDto>>>
{
    public async Task<Result<PagedResult<ResponseBookDto>>> Handle(GetAllBooksQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.Books
        .AsNoTracking()
        .OrderByDescending(b => b.CreatedOn);

        var pagedBook = await query.ToPagedResultAsync(
            request.PageNumber,
            request.PageSize,
            ResponseBookDto.Projection,
            cancellationToken
        );

        return Result<PagedResult<ResponseBookDto>>.Success(pagedBook);
    }
}