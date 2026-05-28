using LibraryMS.Application.Common.Results;
using LibraryMS.Application.DTOs.ClientDto;
using MediatR;

namespace LibraryMS.Application.Features.Client.Queries.GetAllClient;

public sealed record GetAllClientQuery(
    int PageNumber,
    int PageSize
)
    : IRequest<Result<PagedResult<ClientResponseDto>>>;