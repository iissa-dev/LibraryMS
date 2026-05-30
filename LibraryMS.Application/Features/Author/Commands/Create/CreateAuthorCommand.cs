namespace LibraryMS.Application.Features.Author.Commands.Create;

public sealed record CreateAuthorCommand(
    string FirstName,
    string LastName,
    string Biography
) : IRequest<Result>;