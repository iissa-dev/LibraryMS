using LibraryMS.Application.Common.Results;
using LibraryMS.Application.DTOs.BookDtos;
using MediatR;

namespace LibraryMS.Application.Features.Book.Queries.GetByIdWithAuthors;

public sealed record GetByIdWithAuthorsQuery(int Id) : IRequest<Result<ResponseBookDto>>;
