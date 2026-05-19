using LibraryMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryMS.Application.Interfaces.IRepository;

public interface IAppDbContext
{
    DbSet<Country> Countries { get; set; }
    DbSet<Setting> Settings { get; set; }
    DbSet<Book> Books { get; set; }
    DbSet<BookAuthor> BookAuthors { get; set; }
    DbSet<Author> Authors { get; set; }
    DbSet<BookCopy> BookCopies { get; set; }
    DbSet<BorrowingRecord> BorrowingRecords { get; set; }
    DbSet<Client> Clients { get; set; }
    DbSet<Employee> Employees { get; set; }
    DbSet<Person> People { get; set; }
    DbSet<Fine> Fines { get; set; }
    DbSet<RefreshToken> RefreshTokens { get; set; }
    DbSet<Reservation> Reservations { get; set; }
    
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}