using LibraryMS.Domain.Entities;

namespace LibraryMS.Application.Common.Interfaces;

public interface IBookRepository : IGenericRepository<Book>
{
    Task<bool> IsIsbnUniqueAsync(string isbn, CancellationToken cancellationToken = default);
}