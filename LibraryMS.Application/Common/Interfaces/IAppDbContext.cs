using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryMS.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Country> Countries { get; }
    DbSet<Setting> Settings { get; }
    DbSet<Book> Books { get; }
    DbSet<BookAuthor> BookAuthors { get; }
    DbSet<Author> Authors { get; }
    DbSet<BookCopy> BookCopies { get; }
    DbSet<BorrowingRecord> BorrowingRecords { get; }
    DbSet<Client> Clients { get; }
    DbSet<Employee> Employees { get; }
    DbSet<Fine> Fines { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<Reservation> Reservations { get; }

    DbSet<Person> People {get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}