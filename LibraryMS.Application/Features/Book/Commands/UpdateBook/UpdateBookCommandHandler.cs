namespace LibraryMS.Application.Features.Book.Commands.UpdateBook;

public sealed class UpdateBookCommandHandler(IAppDbContext context, IFileService fileService) : IRequestHandler<UpdateBookCommand, Result>
{
    public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await context.Books
        .Include(b => b.BookAuthors)
        .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (book is null)
            return Result.Failure("Book not found.");

        var imageUrl = "";
        if (request.BookImageUrl is null)
        {
            if (book.BookImageUrl is not null)
                fileService.DeleteImage(book.BookImageUrl);
        }
        else
        {
            imageUrl = await fileService.UploadImageAsync(request.BookImageUrl, "books");
        }
        book.UpdateBookDetails(request.Title,
            request.ISBN,
            request.PublishDate,
            (Genre)request.Genre,
            request.AdditionalDetails,
            imageUrl ?? "");

        book.UpdateBookAuthors(request.AuthorIds);

        context.Books.Update(book);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
