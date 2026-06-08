namespace LibraryMS.Application.Features.Settings.Commands;

public sealed record UpdateSettingCommand(
    int SettingId,
    int DefaultBorrowDays,
    decimal DefaultFinePerDay)
: IRequest<Result>;
