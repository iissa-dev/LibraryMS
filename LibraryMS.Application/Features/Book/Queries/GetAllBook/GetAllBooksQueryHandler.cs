using LibraryMS.Application.DTOs.AuthorDto;
using LibraryMS.Application.DTOs.BookDtos;

namespace LibraryMS.Application.Features.Book.Queries.GetAllBook;

public class GetAllBooksQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllBooksQuery, Result<PagedResult<ResponseBookDto>>>
{
    public async Task<Result<PagedResult<ResponseBookDto>>> Handle(GetAllBooksQuery request,
        CancellationToken cancellationToken)
    {
        var (items, total) = await unitOfWork.Books
            .GetPagedProjectedAsync(book => new ResponseBookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Isbn = book.ISBN,
                    PublishDate = book.PublishDate,
                    Genre = book.Genre,
                    AdditionalDetails = book.AdditionalDetails,
                    BookImageUrl = book.BookImageUrl,
                    Authors = book.BookAuthors.Select(a => new AuthorResponseDto
                    {
                        Id = a.Id,
                        FullName = $"{a.Author.FirstName} {a.Author.LastName}"
                    }).ToList()
                },
                request.PageNumber,
                request.PageSize,
                null,
                books => books.OrderByDescending(b => b.CreatedOn), cancellationToken);

        var pageResult = new PagedResult<ResponseBookDto>
        {
            Items = items,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = total,
            TotalPages = (int)Math.Ceiling((double)total / request.PageSize),
        };

        return Result<PagedResult<ResponseBookDto>>.Success(pageResult);
    }
}