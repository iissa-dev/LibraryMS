using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Author.Commands.Create;

public sealed class CreateAuthorCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateAuthorCommand, Result>
{

    public async Task<Result> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Domain.Entities.Author
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Biography = request.Biography
        };

        unitOfWork.Repository<Domain.Entities.Author>().Add(author);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
