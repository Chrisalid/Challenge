using System;
using Challenge.Domain.Entities;
using Challenge.Domain.Interfaces.Repositories;
using Challenge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Challenge.Infrastructure.Repositories;

public class UnitOfWorkRepository : IUnitOfWorkRepository
{
    private readonly ApplicationDbContext _dbContext;
    private IDbContextTransaction? _transaction;

    public IProductRepository Products { get; }
    public IClientRepository Clients { get; }
    public IUserRepository Users { get; }
    public IOrderRepository Orders { get; }
    public IOrderItemRepository OrderItems { get; }

    public UnitOfWorkRepository(ApplicationDbContext dbContext, ProductRepository productRepository)
    {
        _dbContext = dbContext;

        Users = new UserRepository(_dbContext);
        Products = new ProductRepository(_dbContext);
        Clients = new ClientRepository(_dbContext);
        Orders = new OrderRepository(_dbContext);
        OrderItems = new OrderItemRepository(_dbContext);
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction != null) return;

        _transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _dbContext.Dispose();
    }
}
