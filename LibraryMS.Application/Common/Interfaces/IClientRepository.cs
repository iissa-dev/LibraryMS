using LibraryMS.Application.DTOs.ClientDto;
using LibraryMS.Domain.Entities;

namespace LibraryMS.Application.Common.Interfaces;

public interface IClientRepository : IGenericRepository<Client>
{
    Task<(List<ClientResponseDto> Items, int TotalCount )>
        GetClientsWithUsersPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

    Task<ClientResponseDto?> GetClientProfileByIdUserAsync(int userId, CancellationToken cancellationToken);

    Task<Client?> GetClientByUserId(int userId);
    Task<Client?> GetDeletedClientByUserIdAsync(int userId);
}