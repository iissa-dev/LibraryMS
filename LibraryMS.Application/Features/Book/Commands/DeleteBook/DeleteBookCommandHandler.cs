using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Application.Common.Results;
using MediatR;

namespace LibraryMS.Application.Features.Book.Commands.DeleteBook;

public sealed class DeleteBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteBookCommand, Result>
{

    public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await unitOfWork.Books.GetByIdWithCopiesAsync(request.Id, cancellationToken);
        if (book is null)
        {
            return Result.Failure("Book not found.");
        }

        if (book.IsDeleted)
        {
            return Result.Failure("Book is already deleted.");
        }


        unitOfWork.Books.Delete(book);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}
