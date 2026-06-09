using LibraryMS.Domain.Common.Specifications;

namespace LibraryMS.Application.Common.Extensions;

public static class SpecificationExtensions
{
    public static IQueryable<T> Specify<T>(this IQueryable<T> query, BaseSpecification<T> spec)
    {
        return query.Where(spec.Query);
    }
}
