using LibraryMS.Application.DTOs.BookDtos;

namespace LibraryMS.Application.Features.BookCopies.Queries.GetAllCopies;

public sealed record GetAllCopiesQuery(
    int? FilterByStatus,
    int? BookId,
    int PageSize = 10,
    int PageNumber = 1
) : IRequest<Result<PagedResult<ResponseBookCopiesDto>>>;
