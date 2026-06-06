namespace LibraryMS.Application.Features.Author.Commands.Delete;

public sealed class DeleteAuthorCommandHandler(IAppDbContext context) : IRequestHandler<DeleteAuthorCommand, Result>
{

    public async Task<Result> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await context.Authors.SingleOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (author is null)
            return Result.Failure("Author not found.");

        context.Authors.Remove(author);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
