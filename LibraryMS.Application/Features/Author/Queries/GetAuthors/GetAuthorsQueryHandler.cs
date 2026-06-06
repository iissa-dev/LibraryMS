using LibraryMS.Application.Common.Extensions;
using LibraryMS.Application.DTOs.AuthorDto;

namespace LibraryMS.Application.Features.Author.Queries.GetAuthors;

public sealed class GetAuthorsQueryHandler(IAppDbContext context) : IRequestHandler<GetAuthorsQuery, Result<PagedResult<AuthorDto>>>
{
    public async Task<Result<PagedResult<AuthorDto>>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var query = context.Authors.AsNoTracking();

        var pagedResult = await query
            .ToPagedResultAsync(
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                selector: a => new AuthorDto
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Biography = a.Biography
                },
                cancellationToken: cancellationToken
            );
            
        return Result<PagedResult<AuthorDto>>.Success(pagedResult);
    }
}
