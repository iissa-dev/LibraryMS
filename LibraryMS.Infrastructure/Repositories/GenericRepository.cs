using System.Linq.Expressions;
using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<T> DbSet;

    public GenericRepository(AppDbContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        DbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id)
        => await DbSet.FindAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync()
        => await DbSet.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<TResult>> GetProjectionAsync<TResult>(Expression<Func<T, TResult>> selector,
        Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
    {
        var query = DbSet.AsNoTracking();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return await query.Select(selector).ToListAsync();
    }

    public async Task<(IEnumerable<TResult> items, int total)> GetPagedProjectedAsync<TResult>(
        Expression<Func<T, TResult>> selector, int pageNumber, int pageSize,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, CancellationToken cancellationToken = default)
    {
        var query = DbSet.AsNoTracking();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        var total = await query.CountAsync(cancellationToken);
        if (orderBy != null)
        {
            query = orderBy(query);
        }

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(selector)
            .ToListAsync(cancellationToken);

        return (items, total);
    }

    public void Add(T entity)
        => DbSet.Add(entity);

    public void Update(T entity)
        => DbSet.Update(entity);

    public void Delete(T entity)
        => DbSet.Remove(entity);

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        => await DbSet.AnyAsync(predicate);

    public IQueryable<T> Query() => DbSet.AsNoTracking();
}