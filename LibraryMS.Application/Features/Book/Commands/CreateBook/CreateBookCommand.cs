using LibraryMS.Application.Results;
using MediatR;

namespace LibraryMS.Application.Features.Book.Commands.CreateBook;

public sealed record CreateBookCommand(
    string Title,
    string ISBN,
    DateTime PublishDate,
    short Genre,
    string AdditionalDetails,
    string? BookImageUrl,
    List<int> AuthorIds) : IRequest<Result<int>>;