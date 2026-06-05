using LibraryMS.Application.DTOs.AuthorDto;
using LibraryMS.Application.DTOs.BookDtos;

namespace LibraryMS.Application.Features.Book.Queries.GetByIdWithAuthors;

public sealed class GetByIdWithAuthorsQueryHandler(IAppDbContext context)
    : IRequestHandler<GetByIdWithAuthorsQuery, Result<ResponseBookDto>>
{
    public async Task<Result<ResponseBookDto>> Handle(GetByIdWithAuthorsQuery request, CancellationToken cancellationToken)
    {
        var bookDto = await context.Books
            .Where(b => b.Id == request.Id)
            .Select(ResponseBookDto.Projection)
            .FirstOrDefaultAsync(cancellationToken);

        if (bookDto == null)
        {
            return Result<ResponseBookDto>.Failure("Book not found.");
        }

        return Result<ResponseBookDto>.Success(bookDto);
    }
}
