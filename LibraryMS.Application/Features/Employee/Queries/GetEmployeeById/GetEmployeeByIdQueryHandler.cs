using LibraryMS.Application.DTOs.EmployeeDto;

namespace LibraryMS.Application.Features.Employee.Queries.GetEmployeeById;

public class GetEmployeeByIdQueryHandler(IAppDbContext context)
    : IRequestHandler<GetEmployeeByIdQuery, Result<EmployeeResponseDto>>
{
    public async Task<Result<EmployeeResponseDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await context.Employees
            .AsNoTracking()
            .Where(e => e.Id == request.EmployeeId)
            .Select(e => new EmployeeResponseDto
            {
                EmployeeId = e.Id,
                FirstName = e.Person.FirstName,
                LastName = e.Person.LastName,
                Address = e.Person.Address,
                Country = e.Person.Country != null ? e.Person.Country.Name : "Unknown",
                DateOfBirth = e.Person.DateOfBirth,
                EmployeeCode = e.EmployeeCode,
            })
            .SingleOrDefaultAsync(cancellationToken);

        return employee is not null
        ? Result<EmployeeResponseDto>.Success(employee)
        : Result<EmployeeResponseDto>.Failure("Employee not found");
    }
}