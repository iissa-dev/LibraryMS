namespace LibraryMS.Application.Features.Settings.Commands;

public sealed class UpdateSettingCommandValidator : AbstractValidator<UpdateSettingCommand>
{
    public UpdateSettingCommandValidator()
    {
        RuleFor(s => s.SettingId)
            .GreaterThan(0)
            .WithMessage("Setting Id must be vaild Id and grater than zero");

        RuleFor(s => s.DefaultBorrowDays)
            .GreaterThan(0)
            .LessThanOrEqualTo(14)
            .WithMessage("Borrow day can be less than zero or more then 14");

        RuleFor(s => s.DefaultFinePerDay)
            .GreaterThan(0)
            .LessThanOrEqualTo(10)
            .WithMessage("Fine must be more than zero and less than 10$");
    }
}