namespace LibraryMS.Application.Features.Fine.Queries.CheckFineStatus;

public sealed class CheckFineStatusQueryValidator : AbstractValidator<CheckFineStatusQuery>
{
    public CheckFineStatusQueryValidator()
    {
        RuleFor(f => f.BorrowingId)
            .GreaterThan(0)
            .WithMessage("Borrowing Id must be vaild Id and greater than zero");
    }
}