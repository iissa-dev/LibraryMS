using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Infrastructure.Repositories;

public class PersonRepository(AppDbContext context) : GenericRepository<Person>(context), IPersonRepository
{
    public async Task<Person?> GetPersonByUserIdAsync(int UserId)
    {
        var userWithPerson = await Context.Users
        .Include(u => u.Person)
        .FirstOrDefaultAsync(u => u.Id == UserId);

        return userWithPerson?.Person;

    }
}