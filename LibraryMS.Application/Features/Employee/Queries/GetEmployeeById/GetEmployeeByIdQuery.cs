using LibraryMS.Application.Common.Results;
using LibraryMS.Application.DTOs.EmployeeDto;
using MediatR;

namespace LibraryMS.Application.Features.Employee.Queries.GetEmployeeById;

public sealed record GetEmployeeByIdQuery(
    int UserId
    )
    : IRequest<Result<EmployeeResponseDto>>;