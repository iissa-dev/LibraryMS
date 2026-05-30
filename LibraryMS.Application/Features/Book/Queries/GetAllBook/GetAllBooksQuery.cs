using LibraryMS.Application.DTOs.BookDtos;

namespace LibraryMS.Application.Features.Book.Queries.GetAllBook;

public sealed record GetAllBooksQuery(int PageNumber, int PageSize) : IRequest<Result<PagedResult<ResponseBookDto>>>;