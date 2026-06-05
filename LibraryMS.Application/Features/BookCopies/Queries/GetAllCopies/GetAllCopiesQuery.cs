using LibraryMS.Application.DTOs.BookDtos;

namespace LibraryMS.Application.Features.BookCopies.Queries.GetAllCopies;

public sealed record GetAllCopiesQuery(
    int BookId,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<Result<PagedResult<ResponseBookCopiesDto>>>;
