using LibraryMS.Application.Interfaces.IRepository;
using LibraryMS.Domain.Entities;

namespace LibraryMS.Application.Interfaces.IServices;

public interface IBookRepository : IGenericRepository<Book>
{
    Task<bool> IsIsbnUniqueAsync(string isbn, CancellationToken cancellationToken = default);
}