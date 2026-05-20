using LibraryMS.Application.DTOs.ClientDto;
using LibraryMS.Application.Interfaces.IRepository;
using LibraryMS.Application.Result;
using MediatR;

namespace LibraryMS.Application.Features.Client.Queries;

public class GetAllClientQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllClientQuery, Result<PagedResult<ClientResponseDto>>>
{
    public async Task<Result<PagedResult<ClientResponseDto>>> Handle(GetAllClientQuery request,
        CancellationToken cancellationToken)
    {
        var (items, total) = await unitOfWork.Clients
            .GetPagedProjectedAsync(client => new ClientResponseDto
                {
                    Id = client.Id,
                    Address = client.Person.Address,
                    LibraryCardNumber = client.LibraryCardNumber,
                    FirstName = client.Person.FirstName,
                    LastName = client.Person.LastName
                },
                request.PageNumber,
                request.PageSize,
                null,
                clients => clients.OrderByDescending(c => c.CreatedOn),
                cancellationToken);

        var pagedResult = new PagedResult<ClientResponseDto>
        {
            Items = items,
            TotalPages = (int)Math.Ceiling((double)total / request.PageSize),
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = total
        };

        return Result<PagedResult<ClientResponseDto>>.Success(pagedResult);
    }
}