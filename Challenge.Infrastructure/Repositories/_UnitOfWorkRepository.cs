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
    public IAuthTokenRepository AuthTokens { get; }
    public IOrderRepository Orders { get; }
    public IOrderItemRepository OrderItems { get; }

    public UnitOfWorkRepository(
        ApplicationDbContext dbContext,
        IProductRepository productRepository,
        IClientRepository clientRepository,
        IUserRepository userRepository,
        IAuthTokenRepository authTokenRepository,
        IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository)
    {
        _dbContext = dbContext;

        Products = productRepository;
        Clients = clientRepository;
        Users = userRepository;
        AuthTokens = authTokenRepository;
        Orders = orderRepository;
        OrderItems = orderItemRepository;
    }

    public async Task Begin()
    {
        if (_transaction != null) return;

        _transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task Commit()
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
            await Rollback();
            throw;
        }
    }

    public async Task Rollback()
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
