using LibraryMS.Application.Common.Results;
using LibraryMS.Application.DTOs.AuthorDto;
using MediatR;

namespace LibraryMS.Application.Features.Author.Queries.GetAuthorById;

public sealed record GetAuthorByIdQuery(int Id) : IRequest<Result<AuthorDto>>;
