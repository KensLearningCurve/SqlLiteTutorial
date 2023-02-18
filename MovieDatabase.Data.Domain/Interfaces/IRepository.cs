namespace MovieDatabase.Data.Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();

    Task<TEntity> CreateAsync(TEntity entity);

    Task Delete(TEntity entity);

    Task SaveChangesAsync();
}
