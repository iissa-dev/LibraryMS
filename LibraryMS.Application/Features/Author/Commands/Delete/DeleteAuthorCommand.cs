namespace LibraryMS.Application.Features.Author.Commands.Delete;

public sealed record DeleteAuthorCommand(int Id) : IRequest<Result>;
