using LibraryMS.Application.DTOs.ClientDto;
using LibraryMS.Domain.Entities;

namespace LibraryMS.Application.Interfaces.IRepository;

public interface IClientRepository : IGenericRepository<Client>
{
    Task<(List<ClientResponseDto> Items, int TotalCount )>
        GetClientsWithUsersPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}