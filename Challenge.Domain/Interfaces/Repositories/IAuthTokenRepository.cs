using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces.Repositories.Base;

namespace Challenge.Domain.Interfaces.Repositories;

public interface IAuthTokenRepository : IBaseRepository<AuthToken>
{
    Task<AuthToken> GetByUserId(long userId);
    Task<bool> IsTokenValidAsync(string token);
}
