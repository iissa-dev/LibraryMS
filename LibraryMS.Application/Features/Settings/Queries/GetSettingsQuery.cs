namespace LibraryMS.Application.Features.Settings.Queries;


public sealed record SettingsDto(int SettingId, int DefaultBorrowDays, decimal DefaultFinePerDay);
public sealed record GetSettingsQuery : IRequest<Result<SettingsDto>>;
