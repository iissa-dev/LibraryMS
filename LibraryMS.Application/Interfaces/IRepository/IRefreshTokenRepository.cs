using LibraryMS.Domain.Entities;

namespace LibraryMS.Application.Interfaces.IRepository;

public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    Task<RefreshToken?> GetByTokenAsync(string refreshToken);
}