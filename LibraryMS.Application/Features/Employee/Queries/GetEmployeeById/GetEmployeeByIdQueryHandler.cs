using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Application.Common.Results;
using LibraryMS.Application.DTOs.EmployeeDto;
using MediatR;

namespace LibraryMS.Application.Features.Employee.Queries.GetEmployeeById;

public class GetEmployeeByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetEmployeeByIdQuery, Result<EmployeeResponseDto>>
{
    public async Task<Result<EmployeeResponseDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await unitOfWork.Employees.GetEmployeeProfileByIdUserAsync(request.UserId, cancellationToken);

        return employee is not null 
        ? Result<EmployeeResponseDto>.Success(employee) 
        : Result<EmployeeResponseDto>.Failure("Employee not found");
    }
}