using LibraryMS.Application.DTOs.AuthorDto;

namespace LibraryMS.Application.Features.Author.Queries.GetAuthorById;

public sealed record GetAuthorByIdQuery(int Id) : IRequest<Result<AuthorDto>>;
