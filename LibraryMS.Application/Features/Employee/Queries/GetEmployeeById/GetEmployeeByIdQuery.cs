using LibraryMS.Application.Common.DTOs.EmployeeDto;
using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Employee.Queries.GetEmployeeById;

public sealed record GetEmployeeByIdQuery(
    int UserId
    )
    : IRequest<Result<EmployeeResponseDto>>;