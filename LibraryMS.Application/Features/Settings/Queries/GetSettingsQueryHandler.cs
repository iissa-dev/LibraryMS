namespace LibraryMS.Application.Features.Settings.Queries;

public sealed class GetSettingsQueryHandler(IAppDbContext context)
    : IRequestHandler<GetSettingsQuery, Result<SettingsDto>>
{
    public async Task<Result<SettingsDto>> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
    {
        var setting = await context.Settings
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (setting is null)
            return Result<SettingsDto>.Failure("Settings not found in the system");

        var settingsDto = new SettingsDto(setting.Id, setting.DefaultBorrowDays, setting.DefaultFinePerDay);

        return Result<SettingsDto>.Success(settingsDto);
    }
}