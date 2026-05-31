namespace LibraryMS.Application.Features.BookCopies.Command.Create;

public sealed class CreateBookCopyCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateBookCopyCommand, Result>
{
    public async Task<Result> Handle(CreateBookCopyCommand request, CancellationToken cancellationToken)
    {
        var book = await unitOfWork.Books.GetByIdWithCopiesAsync(request.BookId, cancellationToken);
        if (book is null) return Result.Failure("Book not found");

        for (int i = 0; i < request.InitialCopiesCount; i++)
        {
            book.AddCopy();
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
