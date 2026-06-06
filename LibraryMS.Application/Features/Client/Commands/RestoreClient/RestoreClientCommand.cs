namespace LibraryMS.Application.Features.Client.Commands.RestoreClient;

public sealed record RestoreClientCommand(int UserId, int ClientId) : IRequest<Result>;
