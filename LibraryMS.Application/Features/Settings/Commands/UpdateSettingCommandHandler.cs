namespace LibraryMS.Application.Features.Settings.Commands;

public sealed class UpdateSettingCommandHandler(IAppDbContext context)
    : IRequestHandler<UpdateSettingCommand, Result>
{
    public async Task<Result> Handle(UpdateSettingCommand request, CancellationToken cancellationToken)
    {
        var setting = await context.Settings.SingleOrDefaultAsync(s => s.Id == request.SettingId, cancellationToken);
        if (setting is null) return Result.Failure("No settings");



        setting.DefaultBorrowDays = request.DefaultBorrowDays;
        setting.DefaultFinePerDay = request.DefaultFinePerDay;


        context.Settings.Update(setting);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}
