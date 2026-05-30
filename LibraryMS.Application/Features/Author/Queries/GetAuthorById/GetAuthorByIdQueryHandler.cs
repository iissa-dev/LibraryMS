using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Application.Common.Results;
using LibraryMS.Application.DTOs.AuthorDto;
using MediatR;

namespace LibraryMS.Application.Features.Author.Queries.GetAuthorById;

public sealed class GetAuthorByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAuthorByIdQuery, Result<AuthorDto>>
{
    public async Task<Result<AuthorDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await unitOfWork.Repository<Domain.Entities.Author>().GetByIdAsync(request.Id);
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
