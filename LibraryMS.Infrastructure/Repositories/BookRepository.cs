namespace LibraryMS.Infrastructure.Repositories;

public class BookRepository(AppDbContext context) : GenericRepository<Book>(context), IBookRepository
{
    public Task<bool> IsIsbnExistsAsync(string isbn, CancellationToken cancellationToken = default)
    {
        return DbSet
        .IgnoreQueryFilters()
        .AnyAsync(b => b.ISBN == isbn, cancellationToken);
    }

    public Task<Book?> GetDeletedBookByIdWithCopiesAsync(int id, CancellationToken cancellationToken = default)
    {
        return DbSet
        .Include(b => b.Copies)
        .IgnoreQueryFilters()
        .FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted, cancellationToken);
    }

    public Task<Book?> GetByIdWithCopiesAsync(int id, CancellationToken cancellationToken = default)
    {
        return DbSet
        .Include(b => b.Copies)
        .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public Task<Book?> GetByIdWithAuthorsAsync(int id, CancellationToken cancellationToken = default)
    {
        return DbSet
        .Include(b => b.BookAuthors)
        .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }
}