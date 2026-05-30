using System.Linq.Expressions;

namespace LibraryMS.Application.Common.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();

    /// <include file="../../Docs/IGenericDocs.xml" path="/doc/members/member[@name='GetProjectionAsync']/*" />
    Task<IEnumerable<TResult>> GetProjectionAsync<TResult>(
        Expression<Func<T, TResult>> selector,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

    /// <include file="../../Docs/IGenericDocs.xml" path="/doc/members/member[@name='GetPagedProjectedAsync']/*" />
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

    /// <include file="../../Docs/IGenericDocs.xml" path="/doc/members/member[@name='AsQueryable']/*" />
    IQueryable<T> AsQueryable();
}