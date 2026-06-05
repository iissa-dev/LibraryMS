using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LibraryMS.Infrastructure.Interceptors;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>>
        SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
            return ValueTask.FromResult(result);

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Deleted, Entity: ISoftDeleteable softDeleteable })
                continue;

            softDeleteable.Delete();
            entry.State = EntityState.Modified;

            // Modifi only IsDeleted And DeletedOn
            foreach (var property in entry.Properties)
            {
                if (property.Metadata.Name != nameof(ISoftDeleteable.IsDeleted) &&
                    property.Metadata.Name != nameof(ISoftDeleteable.DeletedOn))
                {
                    property.IsModified = false;
                }
            }
        }

        return ValueTask.FromResult(result);
    }
}