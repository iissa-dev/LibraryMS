using LibraryMS.Application.Common.Results;
using LibraryMS.Application.DTOs.BookDtos;
using MediatR;

namespace LibraryMS.Application.Features.Book.Queries.GetAllBook;

public sealed record GetAllBooksQuery(int PageNumber, int PageSize) : IRequest<Result<PagedResult<ResponseBookDto>>>;