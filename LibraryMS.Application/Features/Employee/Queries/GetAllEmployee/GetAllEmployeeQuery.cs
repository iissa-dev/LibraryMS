using LibraryMS.Application.Common.DTOs.EmployeeDto;
using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Employee.Queries.GetAllEmployee;

public sealed record GetAllEmployeeQuery(
    int PageNumber,
    int PageSize
)
    : IRequest<Result<PagedResult<EmployeeResponseDto>>>;