namespace LibraryMS.Application.Features.Author.Commands.Restore;

public sealed class RestoreAuthorCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RestoreAuthorCommand, Result>
{

    public async Task<Result> Handle(RestoreAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await unitOfWork.Repository<Domain.Entities.Author>()
        .AsQueryable()
        .IgnoreQueryFilters()
        .SingleOrDefaultAsync(a => a.Id == request.Id, cancellationToken: cancellationToken);

        if (author is null)
            return Result.Failure("Author not found.");

        if (!author.IsDeleted)
            return Result.Failure("Author is not deleted.");

        author.UnDelete();
        unitOfWork.Repository<Domain.Entities.Author>().Update(author);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
