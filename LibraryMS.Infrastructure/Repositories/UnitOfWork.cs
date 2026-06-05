using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryMS.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _currentTransaction;

    // Cash to save Generic Repositories. Create once in Runtime
    private readonly ConcurrentDictionary<string, object> _repositories;

    // Proprieties
    public IRefreshTokenRepository RefreshTokens { get; }
    public IClientRepository Clients { get; }
    public IEmployeeRepository Employees { get; }
    public IBookCopiesRepository BookCopies { get; }

    public UnitOfWork(AppDbContext context, IRefreshTokenRepository refreshTokenRepository,
        IClientRepository clientRepository, IEmployeeRepository employeesRepository, IBookCopiesRepository bookCopies)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _repositories = new ConcurrentDictionary<string, object>();
        RefreshTokens = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
        Clients = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        Employees = employeesRepository ?? throw new ArgumentNullException(nameof(employeesRepository));
        BookCopies = bookCopies;
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
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        _currentTransaction = await _context.Database.BeginTransactionAsync();
        return _currentTransaction;
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            if (_currentTransaction != null)
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