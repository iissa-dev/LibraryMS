namespace LibraryMS.Application.Features.Fine.Commands.PayFine;

public sealed class PayFineCommandValidator : AbstractValidator<PayFineCommand>
{
    public PayFineCommandValidator()
    {
        RuleFor(f => f.FineId)
            .GreaterThan(0)
            .WithMessage("Fine Id must be vaild Id and greater than zero");
    }
}