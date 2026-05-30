namespace LibraryMS.Application.Features.Book.Commands.RestoreBook;

public sealed record RestoreBookCommand(int Id) : IRequest<Result>;
