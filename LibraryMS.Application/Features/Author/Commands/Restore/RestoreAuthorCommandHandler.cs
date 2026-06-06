namespace LibraryMS.Application.Features.Author.Commands.Restore;

public sealed class RestoreAuthorCommandHandler(IAppDbContext context) : IRequestHandler<RestoreAuthorCommand, Result>
{

    public async Task<Result> Handle(RestoreAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await context.Authors
        .IgnoreQueryFilters()
        .SingleOrDefaultAsync(a => a.Id == request.Id, cancellationToken: cancellationToken);

        if (author is null)
            return Result.Failure("Author not found.");

        if (!author.IsDeleted)
            return Result.Failure("Author is not deleted.");

        author.UnDelete();
        context.Authors.Update(author);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
