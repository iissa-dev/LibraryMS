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
        .AsNoTracking();

        if (request.DeletedData.HasValue && request.DeletedData.Value)
        {
            query = query.IgnoreQueryFilters()
            .Where(b => b.IsDeleted);
        }

        if (request.SearchByGenre is not null)
        {
            query = query.Where(b => b.Genre == (Genre)request.SearchByGenre);
        }

        if (request.SearchByTitle is not null)
        {
            query = query.Where(b => b.Title.Contains(request.SearchByTitle.Trim()));
        }


        var pagedBook = await query
        .OrderByDescending(b => b.CreatedOn)
        .ToPagedResultAsync(
            request.PageNumber,
            request.PageSize,
            ResponseBookDto.Projection,
            cancellationToken
        );

        return Result<PagedResult<ResponseBookDto>>.Success(pagedBook);
    }
}