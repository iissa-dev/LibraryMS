using LibraryMS.Application.DTOs.AuthorDto;

namespace LibraryMS.Application.Features.Author.Queries.GetAuthors;

public sealed record GetAuthorsQuery(
    int PageNumber,
    int PageSize
) : IRequest<Result<PagedResult<AuthorResponseDto>>>;
