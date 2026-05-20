using LibraryMS.Application.Interfaces.IRepository;
using LibraryMS.Application.Result;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Enums;
using MediatR;

namespace LibraryMS.Application.Features.Book.Commands.CreateBook;

public sealed class CreateBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateBookCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var isbnExists = await unitOfWork.Books.IsIsbnUniqueAsync(request.ISBN, cancellationToken);
        if (isbnExists)
            return Result<int>.Failure("The provided ISBN is already registered with another book.");
        
        if(request.AuthorIds.Count == 0) 
            return Result<int>.Failure("At least one author must be assigned to the book.");
        
        var book = new Domain.Entities.Book
        {
            Title = request.Title,
            ISBN = request.ISBN,
            PublishDate = request.PublishDate,
            Genre = (Genre)request.Genre,
            AdditionalDetails = request.AdditionalDetails,
            BookImageUrl = request.BookImageUrl,
            
            Authors = request.AuthorIds.Select(id => new BookAuthor
            {
                AuthorId = id
            }).ToList()
        };

        unitOfWork.Books.Add(book);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(book.Id);
    }
}