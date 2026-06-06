namespace LibraryMS.Application.Features.Client.Commands.UpdateClient;

public sealed record UpdateClientCommand(
    int ClientId,
    string LibraryCardNumber
) : IRequest<Result>;