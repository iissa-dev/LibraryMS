namespace LibraryMS.Application.Features.BookCopies.Command.UpdateStatus;

public sealed class UpdateStatusCopyCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateStatusCopyCommand, Result>
{
    public async Task<Result> Handle(UpdateStatusCopyCommand request, CancellationToken cancellationToken)
    {
        var copy = await unitOfWork.BookCopies.GetByIdAsync(request.BookCopyId);
        if (copy is null) return Result.Failure("Copy Id not found");

        copy.UpdateStatus((CopyStatus)request.CopyStatus);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success;

    }
}