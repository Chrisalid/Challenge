using System;
using System.Threading.Tasks;
using Challenge.Domain.Interfaces.Repositories.Base;
using Challenge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Infrastructure.Repositories.Base;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task CreateAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity).ConfigureAwait(false);

        await SaveChanges();
    }

    public async Task<TEntity?> GetById(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetList()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);

        await SaveChanges();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);

        await SaveChanges();
    }

    private async Task<int> SaveChanges(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
