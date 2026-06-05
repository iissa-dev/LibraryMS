using LibraryMS.Application.DTOs.FineDto;

namespace LibraryMS.Application.Features.Fine.Queries.GetById;

public sealed record GetAllfinesByIdQuery(
    int PageNumber,
    int PageSize,
    int ClientId
) : IRequest<Result<PagedResult<FineDetails>>>;
