namespace LibraryMS.Application.Features.Book.Commands.DeleteBook;

public sealed class DeleteBookCommandHandler(IAppDbContext context) : IRequestHandler<DeleteBookCommand, Result>
{

    public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await context.Books
        .IgnoreQueryFilters()
        .Include(b => b.Copies)
        .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (book is null)
        {
            return Result.Failure("Book not found.");
        }

        if (book.IsDeleted)
        {
            return Result.Failure("Book is already deleted.");
        }


        context.Books.Remove(book);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}
