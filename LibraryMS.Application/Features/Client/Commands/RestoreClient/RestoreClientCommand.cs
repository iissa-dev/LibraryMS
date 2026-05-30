namespace LibraryMS.Application.Features.Client.Commands.RestoreClient;

public sealed record RestoreClientCommand(int UserId) : IRequest<Result>;
