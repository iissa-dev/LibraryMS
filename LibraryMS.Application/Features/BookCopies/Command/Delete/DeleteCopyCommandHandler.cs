
namespace LibraryMS.Application.Features.BookCopies.Command.Delete;

public sealed class DeleteCopyCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCopyCommand, Result>
{
    public async Task<Result> Handle(DeleteCopyCommand request, CancellationToken cancellationToken)
    {
        var copy = await unitOfWork.BookCopies.GetDeletedCopyByIdAsync(request.BookCopyId);
        if (copy is null) return Result.Failure("Copy not found");

        if (copy.IsDeleted) return Result.Failure("Copy already deleted");
        
        unitOfWork.BookCopies.Delete(copy);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}