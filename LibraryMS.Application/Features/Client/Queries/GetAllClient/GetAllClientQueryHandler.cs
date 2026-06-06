using LibraryMS.Application.Common.Extensions;
using LibraryMS.Application.DTOs.ClientDto;

namespace LibraryMS.Application.Features.Client.Queries.GetAllClient;

public class GetAllClientQueryHandler(IAppDbContext context)
    : IRequestHandler<GetAllClientQuery, Result<PagedResult<ClientResponseDto>>>
{
    public async Task<Result<PagedResult<ClientResponseDto>>> Handle(GetAllClientQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.Clients
            .AsNoTracking()
            .OrderByDescending(c => c.CreatedOn);

        var pagedResult = await query
            .ToPagedResultAsync(
                request.PageNumber,
                request.PageSize,
                selector: x => new ClientResponseDto
                {
                    ClientId = x.Id,
                    CreatedOn = x.CreatedOn,
                    LibraryCardNumber = x.LibraryCardNumber,
                    Address = x.Person.Address,
                    FirstName = x.Person.FirstName,
                    LastName = x.Person.LastName,
                    Country = x.Person.Country != null ? x.Person.Country.Name : "Unknown"
                },
                cancellationToken
            );

        return Result<PagedResult<ClientResponseDto>>.Success(pagedResult);
    }
}