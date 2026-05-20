using LibraryMS.Application.Interfaces.IServices;
using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryMS.Application.Interfaces.IRepository;

public interface IUnitOfWork : IDisposable
{
    IBookRepository Books { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    IClientRepository Clients { get; }
    IEmployeeRepository Employees { get; }
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}