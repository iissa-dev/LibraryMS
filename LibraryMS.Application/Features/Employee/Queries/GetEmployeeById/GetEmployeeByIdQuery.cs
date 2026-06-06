using LibraryMS.Application.DTOs.EmployeeDto;

namespace LibraryMS.Application.Features.Employee.Queries.GetEmployeeById;

public sealed record GetEmployeeByIdQuery(
    int EmployeeId
    )
    : IRequest<Result<EmployeeResponseDto>>;