namespace LibraryMS.Application.Features.Fine.Commands.PayFine;

public sealed class PayFineCommandHandler(IAppDbContext context)
    : IRequestHandler<PayFineCommand, Result>
{
    public async Task<Result> Handle(PayFineCommand request, CancellationToken cancellationToken)
    {
        var fine = await context.Fines
            .SingleOrDefaultAsync(f => f.Id == request.FineId, cancellationToken);

        if (fine is null)
            return Result.Failure("Fine not found");

        if (fine.PaymentStatus == PaymentStatus.Paid)
            return Result.Failure("Fine already paid");

        fine.PaymentStatus = PaymentStatus.Paid;
        context.Fines.Update(fine);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
