namespace LibraryMS.Application.Features.Book.Commands.UpdateBook;

public sealed class UpdateBookCommandHandler(IAppDbContext context) : IRequestHandler<UpdateBookCommand, Result>
{
    public  async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await context.Books
        .Include(b => b.BookAuthors)
        .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (book is null)
            return Result.Failure("Book not found.");

        book.UpdateBookDetails(request.Title,
            request.ISBN,
            request.PublishDate,
            (Genre)request.Genre,
            request.AdditionalDetails,
            request.BookImageUrl);

        book.UpdateBookAuthors(request.AuthorIds);

        context.Books.Update(book);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
