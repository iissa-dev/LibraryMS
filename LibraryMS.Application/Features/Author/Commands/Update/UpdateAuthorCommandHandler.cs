namespace LibraryMS.Application.Features.Author.Commands.Update;

public sealed class UpdateAuthorCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateAuthorCommand, Result>
{
    public async Task<Result> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await unitOfWork.Repository<Domain.Entities.Author>().GetByIdAsync(request.Id);
        if (author is null)
        {
            return Result.Failure("Author not found.");
        }

        author.Update(
            request.FirstName,
            request.LastName,
            request.Biography);

        unitOfWork.Repository<Domain.Entities.Author>().Update(author);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}