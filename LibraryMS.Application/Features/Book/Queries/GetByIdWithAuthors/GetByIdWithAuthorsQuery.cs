using LibraryMS.Application.DTOs.BookDtos;

namespace LibraryMS.Application.Features.Book.Queries.GetByIdWithAuthors;

public sealed record GetByIdWithAuthorsQuery(int Id) : IRequest<Result<ResponseBookDto>>;
