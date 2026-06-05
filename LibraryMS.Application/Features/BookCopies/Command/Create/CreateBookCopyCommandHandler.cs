namespace LibraryMS.Application.Features.BookCopies.Command.Create;

public sealed class CreateBookCopyCommandHandler(IUnitOfWork unitOfWork, IAppDbContext context)
    : IRequestHandler<CreateBookCopyCommand, Result>
{
    public async Task<Result> Handle(CreateBookCopyCommand request, CancellationToken cancellationToken)
    {
        var book = await context.Books
        .Include(b => b.Copies)
        .FirstOrDefaultAsync(b => b.Id == request.BookId, cancellationToken);
        
        if (book is null) return Result.Failure("Book not found");

        for (int i = 0; i < request.InitialCopiesCount; i++)
        {
            book.AddCopy();
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
