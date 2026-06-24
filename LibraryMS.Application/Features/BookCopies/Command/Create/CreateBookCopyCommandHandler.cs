namespace LibraryMS.Application.Features.BookCopies.Command.Create;

public sealed class CreateBookCopyCommandHandler(IAppDbContext context, ICodeGeneratorService codeGenerator)
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
            var serialNumber = codeGenerator.GenerateSerialNumber(book.ISBN);
            book.AddCopy(serialNumber);
        }

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
