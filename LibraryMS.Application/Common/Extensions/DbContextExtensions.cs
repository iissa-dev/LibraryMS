namespace LibraryMS.Application.Common.Extensions;

public static class DbContextExtensions
{
    public static async Task<Setting> GetApplicationSettingsAsync(this IAppDbContext context, CancellationToken cancellationToken)
    {
        var setting = await context.Settings
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken)
        ?? throw new InvalidOperationException("Critical Error: Application settings are missing from the database.");
        return setting;
    }
}