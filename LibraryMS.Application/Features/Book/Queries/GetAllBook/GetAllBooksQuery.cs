using LibraryMS.Application.DTOs.BookDtos;
using LibraryMS.Application.Result;
using MediatR;

namespace LibraryMS.Application.Features.Book.Queries.GetAllBook;

public sealed record GetAllBooksQuery(int PageNumber, int PageSize) : IRequest<Result<PagedResult<ResponseBookDto>>>;