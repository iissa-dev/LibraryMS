using LibraryMS.Application.DTOs.ClientDto;

namespace LibraryMS.Infrastructure.Repositories;

public class ClientRepository(AppDbContext context) : GenericRepository<Client>(context), IClientRepository
{
    public async Task<ClientResponseDto?> GetClientProfileByIdUserAsync(int userId, CancellationToken cancellationToken)
    {
        return await Context.Clients
        .AsNoTracking()
        .Where(c => c.UserId == userId)
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
            Address = x.user.Address,
            FirstName = x.user.FirstName,
            LastName = x.user.LastName,
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
                Address = x.user.Address,
                LibraryCardNumber = x.client.LibraryCardNumber,
                FirstName = x.user.FirstName,
                LastName = x.user.LastName,
                CreatedOn = x.client.CreatedOn
            })
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<Client?> GetClientByUserId(int userId)
    {
        var client = await Context.Clients
            .SingleOrDefaultAsync(c => c.UserId == userId);

        return client;
    }

    public async Task<Client?> GetDeletedClientByUserIdAsync(int userId)
    {
        return await Context.Clients
        .IgnoreQueryFilters()
        .FirstOrDefaultAsync(c => c.UserId == userId && c.IsDeleted);
    }
}