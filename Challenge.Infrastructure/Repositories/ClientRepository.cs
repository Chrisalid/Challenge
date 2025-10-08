using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces.Repositories;
using Challenge.Infrastructure.Data;
using Challenge.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Infrastructure.Repositories;

public class ClientRepository(ApplicationDbContext dbContext) : BaseRepository<Client>(dbContext), IClientRepository
{
    protected readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Client> GetByEmail(string email)
    {
        return await _dbContext.Client.Where(u => u.Email == email).FirstOrDefaultAsync();
    }

    public async Task<Client> GetByUserId(long userId)
    {
        return await _dbContext.Client.Where(u => u.User.Id == userId).FirstOrDefaultAsync();
    }
}
