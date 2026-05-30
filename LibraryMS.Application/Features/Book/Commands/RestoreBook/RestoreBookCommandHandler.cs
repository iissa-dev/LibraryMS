namespace LibraryMS.Application.Features.Book.Commands.RestoreBook;

public sealed class RestoreBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RestoreBookCommand, Result>
{

    public async Task<Result> Handle(RestoreBookCommand request, CancellationToken cancellationToken)
    {
        var book = await unitOfWork.Books.GetDeletedBookByIdWithCopiesAsync(request.Id, cancellationToken);
        if (book is null)
        {
            return Result.Failure("Book not found.");
        }

        if (!book.IsDeleted)
        {
            return Result.Failure("Book is not deleted.");
        }

        book.UnDelete();
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}
