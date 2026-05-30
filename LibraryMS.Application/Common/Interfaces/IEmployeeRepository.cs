using LibraryMS.Application.DTOs.EmployeeDto;

namespace LibraryMS.Application.Common.Interfaces;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    Task<(List<EmployeeResponseDto> Items, int TotalCount)> 
    GetEmployeesWithUsersPagedAsync(int PageNumber, int PageSize, CancellationToken cancellationToken);

    Task<EmployeeResponseDto?> GetEmployeeProfileByIdUserAsync(int UserId, CancellationToken cancellationToken);

    Task<Employee?> GetEmployeeByUserIdAsync(int UserId);

    Task<Employee?> GetDeletedEmployeeByIdAsync(int Id);
}