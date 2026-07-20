namespace LibraryMS.Application.Features.Book.Commands.CreateBook;

public sealed class CreateBookCommandHandler(IAppDbContext context, IFileService fileService, ICodeGeneratorService codeGenerator) : IRequestHandler<CreateBookCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {

        await using var transaction = await context.BeginTransactionAsync(cancellationToken);

        try
        {
            if (request.AuthorIds.Count == 0)
                return Result<int>.Failure("At least one author must be assigned to the book.");

            var imageUrl = "";
            if (request.BookImageUrl is not null)
            {
                imageUrl = await fileService.UploadImageAsync(request.BookImageUrl, "books");
            }
            var isbn = codeGenerator.GenerateIsbn();
            var book = new Domain.Entities.Book
            {
                Title = request.Title,
                ISBN = isbn,
                PublishDate = request.PublishDate,
                Genre = (Genre)request.Genre,
                AdditionalDetails = request.AdditionalDetails,
                BookImageUrl = imageUrl ?? "",
            };

            book.AddBookAuthors(request.AuthorIds);

            for (int i = 0; i < request.InitialCopiesCount; i++)
            {
                var serialNumber = codeGenerator.GenerateSerialNumber(book.ISBN);
                book.AddCopy(serialNumber);
            }

            context.Books.Add(book);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return Result<int>.Success(book.Id);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}