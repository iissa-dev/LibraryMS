using LibraryMS.Application.DTOs.BookDtos;

namespace LibraryMS.Application.Features.BookCopies.Queries.GetAllCopies;

public sealed class GetAllCopiesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllCopiesQuery, Result<PagedResult<ResponseBookCopiesDto>>>
{
    public async Task<Result<PagedResult<ResponseBookCopiesDto>>> Handle(GetAllCopiesQuery request, CancellationToken cancellationToken)
    {

        var (items, totalCount) = await unitOfWork.BookCopies
        .GetPagedProjectedAsync
        (
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
            pageNumber: request.PageNumber,
            pageSize: request.PageSize,
            predicate: bc => bc.BookId == request.BookId &&
                (!request.OnlyAvailable.HasValue || !request.OnlyAvailable.Value || bc.CopyStatus == CopyStatus.Available),
            orderBy: bookCopies => bookCopies.OrderBy(bc => bc.Id),
            ignoreQueryFilters: false,
            cancellationToken
        );

        var pagedResult = new PagedResult<ResponseBookCopiesDto>
        {
            Items = items,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize)
        };

        return Result<PagedResult<ResponseBookCopiesDto>>.Success(pagedResult);
    }
}
