namespace LibraryMS.Application.Features.BookCopies.Command.Restore;

public sealed record RestoreCopyCommand(int BookCopyId) : IRequest<Result>;
