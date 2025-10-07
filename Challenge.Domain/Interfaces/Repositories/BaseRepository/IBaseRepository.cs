namespace Challenge.Domain.Interfaces.Repositories.Base;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetList();
    Task<TEntity?> GetById(long id);
    Task DeleteAsync(TEntity entity);
}
