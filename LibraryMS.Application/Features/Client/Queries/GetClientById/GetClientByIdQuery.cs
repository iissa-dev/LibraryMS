using LibraryMS.Application.Common.DTOs.ClientDto;
using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Client.Queries.GetClientById;

public sealed record GetClientByIdQuery(
    int UserId
    )
    : IRequest<Result<ClientResponseDto>>;