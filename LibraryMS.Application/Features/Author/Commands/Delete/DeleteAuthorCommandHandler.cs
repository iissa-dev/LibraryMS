namespace LibraryMS.Application.Features.Author.Commands.Delete;

public sealed class DeleteAuthorCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteAuthorCommand, Result>
{

    public async Task<Result> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await unitOfWork.Repository<Domain.Entities.Author>().GetByIdAsync(request.Id);
        if (author is null)
            return Result.Failure("Author not found.");

        unitOfWork.Repository<Domain.Entities.Author>().Delete(author);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
