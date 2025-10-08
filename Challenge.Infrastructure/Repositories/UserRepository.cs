using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces.Repositories;
using Challenge.Infrastructure.Data;
using Challenge.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext dbContext) : BaseRepository<User>(dbContext), IUserRepository
{
    protected readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<User> GetByEmail(string email)
    {
        return await _dbContext.User.Where(u => u.Email == email).FirstOrDefaultAsync();
    }
}
