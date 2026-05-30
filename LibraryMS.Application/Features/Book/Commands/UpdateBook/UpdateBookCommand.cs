using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Book.Commands.UpdateBook;

public sealed record UpdateBookCommand(
    int Id,
    string Title,
    string ISBN,
    DateTime PublishDate,
    short Genre,
    string AdditionalDetails,
    string BookImageUrl,
    List<int> AuthorIds) : IRequest<Result>;
