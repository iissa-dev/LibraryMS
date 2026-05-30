using LibraryMS.Application.DTOs.EmployeeDto;

namespace LibraryMS.Application.Features.Employee.Queries.GetAllEmployee;

public sealed record GetAllEmployeeQuery(
    int PageNumber,
    int PageSize
)
    : IRequest<Result<PagedResult<EmployeeResponseDto>>>;