using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces.Repositories;
using Challenge.Infrastructure.Data;
using Challenge.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Infrastructure.Repositories;

public class AuthTokenRepository(ApplicationDbContext dbContext) : BaseRepository<AuthToken>(dbContext), IAuthTokenRepository
{
    protected readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<AuthToken> GetByUserId(long userId)
    {
        return await _dbContext.Set<AuthToken>()
            .Where(_ => _.UserId == userId && _.ExpiresAt <= DateTime.Now)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> IsTokenValidAsync(string token)
    {
        var authToken = await _dbContext.AuthToken
            .FirstOrDefaultAsync(t => t.Token == token);

        if (authToken == null || authToken.ExpiresAt < DateTime.UtcNow)
            return false;

        return true;
    }
}
