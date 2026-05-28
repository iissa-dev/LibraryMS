using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Infrastructure.Repositories;

public class BookRepository(AppDbContext context) : GenericRepository<Book>(context), IBookRepository
{
    public Task<bool> IsIsbnUniqueAsync(string isbn, CancellationToken cancellationToken = default)
    {
        return DbSet.AnyAsync(b => b.ISBN == isbn, cancellationToken);
    }
}