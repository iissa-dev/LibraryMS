using LibraryMS.Application.DTOs.AuthorDto;

namespace LibraryMS.Application.Features.Author.Queries.GetAuthorById;

public sealed class GetAuthorByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetAuthorByIdQuery, Result<AuthorDto>>
{
    public async Task<Result<AuthorDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await context.Authors.SingleOrDefaultAsync(a => a.Id == request.Id, cancellationToken: cancellationToken);
        if (author == null)
        {
            return Result<AuthorDto>.Failure("Author not found.");
        }

        var authorDto = new AuthorDto
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            Biography = author.Biography
        };

        return Result<AuthorDto>.Success(authorDto);
    }
}
