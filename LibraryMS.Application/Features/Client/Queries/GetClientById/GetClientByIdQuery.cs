using LibraryMS.Application.DTOs.ClientDto;

namespace LibraryMS.Application.Features.Client.Queries.GetClientById;

public sealed record GetClientByIdQuery(
    int ClientId
    )
    : IRequest<Result<ClientResponseDto>>;