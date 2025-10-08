using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces.Repositories;
using Challenge.Infrastructure.Data;
using Challenge.Infrastructure.Repositories.Base;

namespace Challenge.Infrastructure.Repositories;

public class ProductRepository(ApplicationDbContext dbContext) : BaseRepository<Product>(dbContext), IProductRepository
{
    protected readonly ApplicationDbContext _dbContext = dbContext;
}
