namespace LibraryMS.Application.Common.Interfaces;

public interface IBookRepository : IGenericRepository<Book>
{
    Task<bool> IsIsbnExistsAsync(string isbn, CancellationToken cancellationToken = default);
    Task<Book?> GetDeletedBookByIdWithCopiesAsync(int id, CancellationToken cancellationToken = default);
    Task<Book?> GetByIdWithCopiesAsync(int id, CancellationToken cancellationToken = default);
    Task<Book?> GetByIdWithAuthorsAsync(int id, CancellationToken cancellationToken = default);
}