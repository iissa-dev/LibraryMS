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
        var (items, totalCount) =
            await unitOfWork.Clients.GetClientsWithUsersPagedAsync(request.PageNumber, request.PageSize,
                cancellationToken);
        var pagedResult = new PagedResult<ClientResponseDto>
        {
            Items = items,
            TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize),
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };

        return Result<PagedResult<ClientResponseDto>>.Success(pagedResult);
    }
}