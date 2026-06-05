namespace LibraryMS.Application.Features.BookCopies.Command.UpdateStatus;

public sealed class UpdateStatusCopyCommandHandler(IAppDbContext context)
    : IRequestHandler<UpdateStatusCopyCommand, Result>
{
    public async Task<Result> Handle(UpdateStatusCopyCommand request, CancellationToken cancellationToken)
    {
        var copy = await context.BookCopies.FindAsync(request.BookCopyId, cancellationToken);
        if (copy is null) return Result.Failure("Copy Id not found");

        copy.UpdateStatus((CopyStatus)request.CopyStatus);

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success;

    }
}