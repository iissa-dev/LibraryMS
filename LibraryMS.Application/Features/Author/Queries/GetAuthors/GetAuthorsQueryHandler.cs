using LibraryMS.Application.Common.Extensions;
using LibraryMS.Application.DTOs.AuthorDto;

namespace LibraryMS.Application.Features.Author.Queries.GetAuthors;

public sealed class GetAuthorsQueryHandler(IAppDbContext context) : IRequestHandler<GetAuthorsQuery, Result<PagedResult<AuthorResponseDto>>>
{
    public async Task<Result<PagedResult<AuthorResponseDto>>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var query = context.Authors.AsNoTracking();

        var pagedResult = await query
            .ToPagedResultAsync(
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                selector: a => new AuthorResponseDto
                {
                    Id = a.Id,
                    FullName = $"{a.FirstName} {a.LastName}"
                },
                cancellationToken: cancellationToken
            );

        return Result<PagedResult<AuthorResponseDto>>.Success(pagedResult);
    }
}
