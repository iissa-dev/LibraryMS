using LibraryMS.Application.Common.DTOs.ClientDto;
using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Infrastructure.Repositories;

public class ClientRepository(AppDbContext context) : GenericRepository<Client>(context), IClientRepository
{
    public async Task<ClientResponseDto?> GetClientProfileByIdUserAsync(int UserId, CancellationToken cancellationToken)
    {
        return await Context.Clients
        .AsNoTracking()
        .Where(c => c.UserId == UserId)
        .Join(Context.Users,
        client => client.UserId,
        user => user.Id,
        (client, user) => new { client, user })
        .Select(x => new ClientResponseDto
        {
            ClientId = x.client.Id,
            CreatedOn = x.client.CreatedOn,
            LibraryCardNumber = x.client.LibraryCardNumber,
            UserId = x.user.Id,
            Username = x.user.UserName ?? "",
            Email = x.user.Email ?? "",
            PhoneNumber = x.user.PhoneNumber ?? "",
            Address = x.user.Person.Address,
            FirstName = x.user.Person.FirstName,
            LastName = x.user.Person.LastName,
        })
        .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<(List<ClientResponseDto> Items, int TotalCount)> GetClientsWithUsersPagedAsync(int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var totalCount = await Context.Clients.CountAsync(cancellationToken);

        var items = await Context.Clients
            .AsNoTracking()
            .Join(Context.Users,
                client => client.UserId,
                user => user.Id,
                (client, user) => new { client, user })
                 .OrderByDescending(c => c.client.CreatedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ClientResponseDto
            {
                ClientId = x.client.Id,
                UserId = x.user.Id,
                Username = x.user.UserName ?? "",
                Email = x.user.Email ?? "",
                PhoneNumber = x.user.PhoneNumber ?? "",
                Address = x.user.Person.Address,
                LibraryCardNumber = x.client.LibraryCardNumber,
                FirstName = x.user.Person.FirstName,
                LastName = x.user.Person.LastName,
                CreatedOn = x.client.CreatedOn
            })
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }
}