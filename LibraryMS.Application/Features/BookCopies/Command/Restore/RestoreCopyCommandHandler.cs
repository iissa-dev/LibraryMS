namespace LibraryMS.Application.Features.BookCopies.Command.Restore;

public sealed class RestoreCopyCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RestoreCopyCommand, Result>
{
    public async Task<Result> Handle(RestoreCopyCommand request, CancellationToken cancellationToken)
    {
        var copy = await unitOfWork.BookCopies.GetDeletedCopyByIdAsync(request.BookCopyId);
        if (copy is null) return Result.Failure("Copy not found");

        copy.UnDelete();
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
