namespace LibraryMS.Application.Features.Book.Commands.UpdateBook;

public sealed class UpdateBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateBookCommand, Result>
{
    public  async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await unitOfWork.Books.GetByIdWithAuthorsAsync(request.Id, cancellationToken);
        if (book is null)
            return Result.Failure("Book not found.");

        book.UpdateBookDetails(request.Title,
            request.ISBN,
            request.PublishDate,
            (Genre)request.Genre,
            request.AdditionalDetails,
            request.BookImageUrl);

        book.UpdateBookAuthors(request.AuthorIds);

        unitOfWork.Books.Update(book);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
