using LibraryMS.Application.Common.Results;
using LibraryMS.Application.DTOs.AuthorDto;
using MediatR;

namespace LibraryMS.Application.Features.Author.Queries.GetAuthors;

public sealed record GetAuthorsQuery(
    int PageNumber,
    int PageSize
) : IRequest<Result<PagedResult<AuthorDto>>>;
