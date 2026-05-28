using LibraryMS.Domain.Entities;

namespace LibraryMS.Application.Common.Interfaces;

public interface IPersonRepository : IGenericRepository<Person>
{
    Task<Person?> GetPersonByUserIdAsync(int UserId);
}