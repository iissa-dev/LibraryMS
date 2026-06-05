using LibraryMS.Application.DTOs.BorrowDto;
namespace LibraryMS.Application.Features.Borrowing.Queries.GetFullBorrowDetailsById;

public sealed record GetFullBorrowDetailsQuery(
    int PageNumber,
    int PageSize,
    int ClientId
) : IRequest<Result<PagedResult<BorrowDetails>>>;
