using LibraryMS.Application.DTOs.FineDto;

namespace LibraryMS.Application.Features.Fine.Queries.CheckFineStatus;

public sealed record CheckFineStatusQuery(int BorrowingId) : IRequest<Result<FineStatusDto>>;
