using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces.Repositories;
using Challenge.Infrastructure.Data;
using Challenge.Infrastructure.Repositories.Base;

namespace Challenge.Infrastructure.Repositories;

public class OrderItemRepository(ApplicationDbContext dbContext) : BaseRepository<OrderItem>(dbContext), IOrderItemRepository
{
    protected readonly ApplicationDbContext _dbContext = dbContext;
}
