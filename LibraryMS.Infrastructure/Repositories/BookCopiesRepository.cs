namespace LibraryMS.Infrastructure.Repositories;

public class BookCopiesRepository(AppDbContext context) : GenericRepository<BookCopy>(context), IBookCopiesRepository
{
    public async Task<BookCopy?> GetDeletedCopyByIdAsync(int BookCopyId)
    {
        return await DbSet
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(bc => bc.Id == BookCopyId);
    }
}