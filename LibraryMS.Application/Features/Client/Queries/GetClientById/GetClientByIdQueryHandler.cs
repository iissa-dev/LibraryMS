using LibraryMS.Application.DTOs.ClientDto;

namespace LibraryMS.Application.Features.Client.Queries.GetClientById;

public class GetClientByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetClientByIdQuery, Result<ClientResponseDto>>
{
    public async Task<Result<ClientResponseDto>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await unitOfWork.Clients.GetClientProfileByIdUserAsync(request.UserId, cancellationToken);

        return client is not null 
        ? Result<ClientResponseDto>.Success(client) 
        : Result<ClientResponseDto>.Failure("Employee not found");
    }
}