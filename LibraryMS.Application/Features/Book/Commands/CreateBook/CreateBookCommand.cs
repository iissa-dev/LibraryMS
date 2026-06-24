using Microsoft.AspNetCore.Http;

namespace LibraryMS.Application.Features.Book.Commands.CreateBook;

public sealed record CreateBookCommand(
    string Title,
    DateTime PublishDate,
    short Genre,
    string AdditionalDetails,
    IFormFile? BookImageUrl,
    List<int> AuthorIds,
    int InitialCopiesCount
) : IRequest<Result<int>>;