using LibraryMS.Application.Common.Interfaces;
using LibraryMS.Domain.Entities;
using LibraryMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryMS.Infrastructure.Repositories;

public class RefreshTokenRepository(AppDbContext context)
    : GenericRepository<RefreshToken>(context), IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByTokenAsync(string refreshToken)
    {
        return await DbSet
            .FirstOrDefaultAsync(rt => rt.RefreshTokenJwt == refreshToken);
    }
}