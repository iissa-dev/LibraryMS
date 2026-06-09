using System.Linq.Expressions;

namespace LibraryMS.Domain.Common.Specifications;

public abstract class BaseSpecification<T>
{
    public Expression<Func<T, bool>> Query { get; protected set; } = null!;
}