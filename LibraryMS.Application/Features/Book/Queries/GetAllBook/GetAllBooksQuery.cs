using LibraryMS.Application.DTOs.BookDtos;

namespace LibraryMS.Application.Features.Book.Queries.GetAllBook;

public sealed record GetAllBooksQuery(
    int PageNumber,
    int PageSize,
    string? SearchByTitle,
    int? SearchByGenre,
    bool? DeletedData = false) : IRequest<Result<PagedResult<ResponseBookDto>>>;