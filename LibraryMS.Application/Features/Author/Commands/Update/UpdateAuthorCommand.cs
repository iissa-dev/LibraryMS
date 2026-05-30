namespace LibraryMS.Application.Features.Author.Commands.Update;

public sealed record UpdateAuthorCommand
(
    int Id,
    string FirstName,
    string LastName,
    string Biography
) : IRequest<Result>;
