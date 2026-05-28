using LibraryMS.Application.Common.DTOs.EmployeeDto;
using LibraryMS.Domain.Entities;

namespace LibraryMS.Application.Common.Interfaces;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    Task<(List<EmployeeResponseDto> Items, int TotalCount)> 
    GetEmployeesWithUsersPagedAsync(int PageNumber, int PageSize, CancellationToken cancellationToken);

    Task<EmployeeResponseDto?> GetEmployeeProfileByIdUserAsync(int UserId, CancellationToken cancellationToken);
}