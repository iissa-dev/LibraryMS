using FluentValidation;
using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Application.Common.Results;
using LibraryMS.Application.DTOs.AuthorDto;
using LibraryMS.Application.DTOs.BookDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Application.Features.Book.Queries.GetByIdWithAuthors;

public sealed class GetByIdWithAuthorsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetByIdWithAuthorsQuery, Result<ResponseBookDto>>
{
    public async Task<Result<ResponseBookDto>> Handle(GetByIdWithAuthorsQuery request, CancellationToken cancellationToken)
    {
        var bookDto = await unitOfWork.Books
            .AsQueryable()
            .Where(b => b.Id == request.Id)
            .Select(book => new ResponseBookDto
            {
                Id = book.Id,
                Title = book.Title,
                Isbn = book.ISBN,
                PublishDate = book.PublishDate,
                Genre = book.Genre,
                AdditionalDetails = book.AdditionalDetails,
                BookImageUrl = book.BookImageUrl,
                Authors = book.BookAuthors.Select(a => new AuthorResponseDto
                {
                    Id = a.Id,
                    FullName = $"{a.Author.FirstName} {a.Author.LastName}"
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (bookDto == null)
        {
            return Result<ResponseBookDto>.Failure("Book not found.");
        }

        return Result<ResponseBookDto>.Success(bookDto);
    }
}
