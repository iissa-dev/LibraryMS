namespace LibraryMS.Application.Features.Author.Commands.Create;

public sealed class CreateAuthorCommandHandler(IAppDbContext context) : IRequestHandler<CreateAuthorCommand, Result>
{

    public async Task<Result> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Domain.Entities.Author
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Biography = request.Biography
        };

        context.Authors.Add(author);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
