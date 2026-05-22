using System.Linq.Expressions;

namespace LibraryMS.Application.Interfaces.IRepository;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    /// <summary>
    /// Retrieves a filtered, sorted, and projected list of read-only data directly from the database.
    /// </summary>
    /// <typeparam name="TResult">The DTO or target type to project the data into.</typeparam>
    /// <param name="selector">The mapping expression (Projection) to select specific columns.</param>
    /// <param name="predicate">Optional filter conditions (Where clause).</param>
    /// <param name="orderBy">Optional sorting logic (OrderBy/ThenBy clauses).</param>
    /// <returns>An asynchronous collection of projected results (<typeparamref name="TResult"/>).</returns>
    Task<IEnumerable<TResult>> GetProjectionAsync<TResult>
    (Expression<Func<T, TResult>> selector,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

    Task<(IEnumerable<TResult> items, int total)> GetPagedProjectedAsync<TResult>(
        Expression<Func<T, TResult>> selector,
        int pageNumber,
        int pageSize,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        CancellationToken cancellationToken = default);

    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);

    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    IQueryable<T> Query();
}