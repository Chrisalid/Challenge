using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces.Repositories;
using Challenge.Infrastructure.Data;
using Challenge.Infrastructure.Repositories.Base;

namespace Challenge.Infrastructure.Repositories;

public class OrderRepository(ApplicationDbContext dbContext) : BaseRepository<Order>(dbContext), IOrderRepository
{
    protected readonly ApplicationDbContext _dbContext = dbContext;
}
