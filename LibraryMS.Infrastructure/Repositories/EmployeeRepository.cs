using System.Data;
using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Application.DTOs.EmployeeDto;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Infrastructure.Repositories;

public class EmployeeRepository(AppDbContext context) : GenericRepository<Employee>(context), IEmployeeRepository
{
    public async Task<EmployeeResponseDto?> GetEmployeeProfileByIdUserAsync(int UserId, CancellationToken cancellationToken)
    {
        return await Context.Employees
        .AsNoTracking()
        .Where(e => e.UserId == UserId)
        .Join(Context.Users,
        employee => employee.UserId,
        user => user.Id,
        (employee, user) => new {employee, user})
        .Select(x => new EmployeeResponseDto
        {
            Id = x.employee.Id,
            EmployeeCode = x.employee.EmployeeCode,
            CreatedOn = x.employee.CreatedOn,
            
            UserId = x.user.Id,
            Username = x.user.UserName ?? "",
            Email = x.user.Email ?? "",
            PhoneNumber = x.user.PhoneNumber ?? "",
            
            FirstName = x.user.FirstName,
            LastName = x.user.LastName,
            Address = x.user.Address
        })
        .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<(List<EmployeeResponseDto> Items, int TotalCount)> GetEmployeesWithUsersPagedAsync(int PageNumber, int PageSize, CancellationToken cancellationToken)
    {
        var totalCount = await Context.Employees.CountAsync(cancellationToken);

        var items = await Context.Employees
            .AsNoTracking()
            .Join(Context.Users,
            employee => employee.UserId,
            user => user.Id,
            (employee, user) => new { employee, user })
            .OrderByDescending(e => e.employee.CreatedOn)
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize)
            .Select(x => new EmployeeResponseDto
            {
                Id = x.employee.Id,
                UserId = x.user.Id,
                Username = x.user.UserName ?? "",
                Email = x.user.Email ?? "",
                PhoneNumber = x.user.PhoneNumber ?? "",
                EmployeeCode = x.employee.EmployeeCode,
                CreatedOn = x.employee.CreatedOn,

                FirstName = x.user.FirstName,
                LastName = x.user.LastName,
                Address = x.user.Address
            })
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }
}