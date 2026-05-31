namespace LibraryMS.Application.Common.Interfaces;

public interface IBookCopiesRepository : IGenericRepository<BookCopy>
{
    Task<BookCopy?> GetDeletedCopyByIdAsync(int BookCopyId);
}