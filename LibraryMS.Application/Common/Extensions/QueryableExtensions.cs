using System.Linq.Expressions;

namespace LibraryMS.Application.Common.Extensions;

public static class QueryableExtensions
{
    public static async Task<PagedResult<TDestination>> ToPagedResultAsync<TSource, TDestination>(
        this IQueryable<TSource> query,
        int pageNumber,
        int pageSize,
        Expression<Func<TSource, TDestination>> selector,
        CancellationToken cancellationToken = default
    )
    {
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
        .Select(selector)
        .Skip((pageNumber - 1) * 10)
        .Take(pageSize)
        .ToListAsync(cancellationToken);


        return new PagedResult<TDestination>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        };
    }
}