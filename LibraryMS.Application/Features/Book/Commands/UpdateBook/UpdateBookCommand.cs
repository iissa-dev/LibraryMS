using Microsoft.AspNetCore.Http;

namespace LibraryMS.Application.Features.Book.Commands.UpdateBook;

public sealed record UpdateBookCommand(
    int Id,
    string Title,
    string ISBN,
    DateTime PublishDate,
    short Genre,
    string AdditionalDetails,
    IFormFile? BookImageUrl,
    List<int> AuthorIds) : IRequest<Result>;
