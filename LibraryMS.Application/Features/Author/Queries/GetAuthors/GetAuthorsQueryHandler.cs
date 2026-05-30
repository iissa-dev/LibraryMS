using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Application.Common.Results;
using LibraryMS.Application.DTOs.AuthorDto;
using MediatR;

namespace LibraryMS.Application.Features.Author.Queries.GetAuthors;

public sealed class GetAuthorsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAuthorsQuery, Result<PagedResult<AuthorDto>>>
{
    public async Task<Result<PagedResult<AuthorDto>>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var (authors, totalCount) = await unitOfWork.Repository<Domain.Entities.Author>()
        .GetPagedProjectedAsync(
            selector: a => new AuthorDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Biography = a.Biography
            },
            pageNumber: request.PageNumber,
            pageSize: request.PageSize,
            null, // no filter
             orderBy: a => a.OrderBy(a => a.LastName).ThenBy(a => a.FirstName),
             cancellationToken: cancellationToken
        );

        var pagedResult = new PagedResult<AuthorDto>
        {
            Items = authors,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
        };

        return Result<PagedResult<AuthorDto>>.Success(pagedResult);
    }
}
