namespace LibraryMS.Application.Features.Author.Commands.Update;

public sealed class UpdateAuthorCommandHandler(IAppDbContext context) : IRequestHandler<UpdateAuthorCommand, Result>
{
    public async Task<Result> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await context.Authors.SingleOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (author is null)
        {
            return Result.Failure("Author not found.");
        }

        author.Update(
            request.FirstName,
            request.LastName,
            request.Biography);

        context.Authors.Update(author);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}