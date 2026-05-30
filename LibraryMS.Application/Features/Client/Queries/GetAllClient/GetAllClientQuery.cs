using LibraryMS.Application.DTOs.ClientDto;

namespace LibraryMS.Application.Features.Client.Queries.GetAllClient;

public sealed record GetAllClientQuery(
    int PageNumber,
    int PageSize
)
    : IRequest<Result<PagedResult<ClientResponseDto>>>;