using System.Collections.Concurrent;
using LibraryMS.Application.Interfaces.IRepository;
using LibraryMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryMS.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _currentTransaction;
    
    // Cash to save Generic Repositories. Create once in Runtime
    private readonly ConcurrentDictionary<string, object> _repositories;

    public UnitOfWork(AppDbContext context)
    {
        _context = context ??  throw new ArgumentNullException(nameof(context));
        _repositories = new ConcurrentDictionary<string, object>();
    }
    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity).Name;

        return (IGenericRepository<TEntity>)_repositories.GetOrAdd(type, _ =>
            new GenericRepository<TEntity>(_context));
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    // Transaction Management
    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction != null) return;

        _currentTransaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            if(_currentTransaction != null)
                await _currentTransaction.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            if (_currentTransaction != null)
                await _currentTransaction.RollbackAsync();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public void Dispose()
    {
        _context.Dispose();
        _currentTransaction?.Dispose();
        GC.SuppressFinalize(this);
    }
}