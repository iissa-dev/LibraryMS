using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryMS.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBookRepository Books { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    IClientRepository Clients { get; }
    IEmployeeRepository Employees { get; }
    IBookCopiesRepository BookCopies { get; }
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}