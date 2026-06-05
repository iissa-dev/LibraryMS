
namespace LibraryMS.Application.Features.BookCopies.Command.Delete;

public sealed class DeleteCopyCommandHandler(IAppDbContext context)
    : IRequestHandler<DeleteCopyCommand, Result>
{
    public async Task<Result> Handle(DeleteCopyCommand request, CancellationToken cancellationToken)
    {
        var copy = await context.BookCopies
        .IgnoreQueryFilters()
        .FirstOrDefaultAsync(bc => bc.Id == request.BookCopyId, cancellationToken);

        if (copy is null) return Result.Failure("Copy not found");

        if (copy.IsDeleted) return Result.Failure("Copy already deleted");

        context.BookCopies.Remove(copy);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}