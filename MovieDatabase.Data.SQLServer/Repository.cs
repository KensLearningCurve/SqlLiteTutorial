using MovieDatabase.Data.Domain.Interfaces;

namespace MovieDatabase.Data.SQLServer;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DataContext dbContext;

    public Repository(DataContext context)
    {
        this.dbContext = context;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await dbContext.Set<TEntity>().AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task Delete(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public IQueryable<TEntity> GetAll()
    {
        return dbContext.Set<TEntity>();
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}
