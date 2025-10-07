using Challenge.Domain.Entities;

namespace Challenge.Domain.Interfaces.Repositories;

public interface IUnitOfWorkRepository
{
    IProductRepository Products { get; }
    IClientRepository Clients { get; }
    IOrderRepository Orders { get; }
    IOrderItemRepository OrderItems { get; }
    IUserRepository Users { get; }

    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
