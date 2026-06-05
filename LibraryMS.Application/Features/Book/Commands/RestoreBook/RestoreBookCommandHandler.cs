namespace LibraryMS.Application.Features.Book.Commands.RestoreBook;

public sealed class RestoreBookCommandHandler(IAppDbContext context) : IRequestHandler<RestoreBookCommand, Result>
{

    public async Task<Result> Handle(RestoreBookCommand request, CancellationToken cancellationToken)
    {
        var book = await context.Books
        .Include(b => b.Copies)
        .IgnoreQueryFilters()
        .FirstOrDefaultAsync(b => b.Id == request.Id && b.IsDeleted, cancellationToken);

        if (book is null)
        {
            return Result.Failure("Book not found.");
        }

        if (!book.IsDeleted)
        {
            return Result.Failure("Book is not deleted.");
        }

        book.UnDelete();
        
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}
