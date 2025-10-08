using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces.Repositories.Base;

namespace Challenge.Domain.Interfaces.Repositories;

public interface IClientRepository : IBaseRepository<Client>
{
    Task<Client> GetByEmail(string email);
    Task<Client> GetByUserId(long userId);
}
