using LibraryMS.Application.DTOs.ClientDto;
using LibraryMS.Application.Interfaces.IRepository;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Infrastructure.Repositories;

public class ClientRepository(AppDbContext context) : GenericRepository<Client>(context), IClientRepository
{
    public async Task<(List<ClientResponseDto> Items, int TotalCount)> GetClientsWithUsersPagedAsync(int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var totalCount = await Context.Clients.CountAsync(cancellationToken);

        var items = await Context.Clients
            .Join(Context.Users,
                client => client.UserId,
                user => user.Id,
                (client, user) => new ClientResponseDto
                {
                    Id = client.Id,
                    UserId = user.Id,
                    Username = user.UserName ?? "",
                    Email = user.Email ?? "",
                    PhoneNumber = user.PhoneNumber ?? "",
                    Address = user.Person.Address,
                    LibraryCardNumber = client.LibraryCardNumber,
                    FirstName = user.Person.FirstName,
                    LastName = user.Person.LastName,
                    CreatedOn = client.CreatedOn
                }).OrderByDescending(c => c.CreatedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }
}