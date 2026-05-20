using LibraryMS.Application.Interfaces.IRepository;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Infrastructure.Repositories;

public class RefreshTokenRepository(AppDbContext context)
    : GenericRepository<RefreshToken>(context), IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByTokenWithUserAsync(string refreshToken)
    {
        return await DbSet
            .FirstOrDefaultAsync(rt => rt.RefreshTokenJwt == refreshToken);
    }
}