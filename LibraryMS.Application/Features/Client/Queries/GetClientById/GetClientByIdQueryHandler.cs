using LibraryMS.Application.DTOs.ClientDto;

namespace LibraryMS.Application.Features.Client.Queries.GetClientById;

public class GetClientByIdQueryHandler(IAppDbContext context)
    : IRequestHandler<GetClientByIdQuery, Result<ClientResponseDto>>
{
    public async Task<Result<ClientResponseDto>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await context.Clients
            .AsNoTracking()
            .Where(c => c.Id == request.ClientId)
            .Select(x => new ClientResponseDto
            {
                ClientId = x.Id,
                CreatedOn = x.CreatedOn,
                LibraryCardNumber = x.LibraryCardNumber,
                Address = x.Person.Address,
                FirstName = x.Person.FirstName,
                LastName = x.Person.LastName,
                Country = x.Person.Country != null ? x.Person.Country.Name : "Unknown"
            })
            .FirstOrDefaultAsync(cancellationToken);

        return client is not null
        ? Result<ClientResponseDto>.Success(client)
        : Result<ClientResponseDto>.Failure("Employee not found");
    }
}