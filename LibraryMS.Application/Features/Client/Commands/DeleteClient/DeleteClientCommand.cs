namespace LibraryMS.Application.Features.Client.Commands.DeleteClient;

public sealed record DeleteClientCommand(int UserId) : IRequest<Result>;
