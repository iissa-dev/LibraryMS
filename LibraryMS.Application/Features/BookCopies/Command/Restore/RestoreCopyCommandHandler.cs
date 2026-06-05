namespace LibraryMS.Application.Features.BookCopies.Command.Restore;

public sealed class RestoreCopyCommandHandler(IAppDbContext context)
    : IRequestHandler<RestoreCopyCommand, Result>
{
    public async Task<Result> Handle(RestoreCopyCommand request, CancellationToken cancellationToken)
    {
        var copy = await context.BookCopies
        .IgnoreQueryFilters()
        .FirstOrDefaultAsync(bc => bc.Id == request.BookCopyId, cancellationToken);
        if (copy is null) return Result.Failure("Copy not found");

        copy.UnDelete();
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
