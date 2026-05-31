
namespace LibraryMS.Application.Features.BookCopies.Command.Delete;

public sealed record DeleteCopyCommand(
    int BookCopyId
) : IRequest<Result>;
