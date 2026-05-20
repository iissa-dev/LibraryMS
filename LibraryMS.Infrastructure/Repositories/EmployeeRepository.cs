using LibraryMS.Application.Interfaces.IRepository;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Data;

namespace LibraryMS.Infrastructure.Repositories;

public class EmployeeRepository(AppDbContext context) : GenericRepository<Employee>(context), IEmployeeRepository;