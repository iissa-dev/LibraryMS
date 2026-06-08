namespace LibraryMS.Application.Features.Book.Commands.CreateBook;

public sealed class CreateBookCommandHandler(IAppDbContext context) : IRequestHandler<CreateBookCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var isbnExists = await context.Books
        .IgnoreQueryFilters()
        .AnyAsync(b => b.ISBN == request.ISBN, cancellationToken);

        if (isbnExists)
            return Result<int>.Failure("The provided ISBN is already registered with another book.");

        if (request.AuthorIds.Count == 0)
            return Result<int>.Failure("At least one author must be assigned to the book.");

        var book = new Domain.Entities.Book
        {
            Title = request.Title,
            ISBN = request.ISBN,
            PublishDate = request.PublishDate,
            Genre = (Genre)request.Genre,
            AdditionalDetails = request.AdditionalDetails,
            BookImageUrl = request.BookImageUrl,
        };

        book.AddBookAuthors(request.AuthorIds);

        for (int i = 0; i < request.InitialCopiesCount; i++)
        {
            book.AddCopy();
        }

        context.Books.Add(book);
        await context.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(book.Id);
    }
}