using System.Text.Json.Serialization;

namespace LibraryMS.Application.Features.BookCopies.Command.Create;

public sealed record CreateBookCopyCommand(
    [property: JsonIgnore] int BookId,
    int InitialCopiesCount
) : IRequest<Result>;
