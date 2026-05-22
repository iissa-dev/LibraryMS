using LibraryMS.Application.DTOs.ClientDto;
using LibraryMS.Application.Results;
using MediatR;

namespace LibraryMS.Application.Features.Client.Queries;

public sealed record GetAllClientQuery(
    int PageNumber,
    int PageSize
)
    : IRequest<Result<PagedResult<ClientResponseDto>>>;