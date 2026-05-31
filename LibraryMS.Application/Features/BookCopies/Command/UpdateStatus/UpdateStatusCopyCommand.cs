namespace LibraryMS.Application.Features.BookCopies.Command.UpdateStatus;

public sealed record UpdateStatusCopyCommand(
    [property: JsonIgnore] int BookCopyId,
    int CopyStatus
) : IRequest<Result>;
