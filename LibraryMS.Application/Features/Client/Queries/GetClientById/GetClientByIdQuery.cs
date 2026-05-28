using LibraryMS.Application.Common.Results;
using LibraryMS.Application.DTOs.ClientDto;
using MediatR;

namespace LibraryMS.Application.Features.Client.Queries.GetClientById;

public sealed record GetClientByIdQuery(
    int UserId
    )
    : IRequest<Result<ClientResponseDto>>;