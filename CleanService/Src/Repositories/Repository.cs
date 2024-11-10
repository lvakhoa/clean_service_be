using System.Linq.Expressions;
using CleanService.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Repositories;

public class Repository<TEntity, TPartialEntity> : IRepository<TEntity, TPartialEntity>
    where TEntity : class, new() where TPartialEntity : class
{
    private readonly CleanServiceContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(CleanServiceContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var result = await _dbSet.AddAsync(entity);
        return result.Entity;
    }

    public async Task AddManyAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void DeleteMany(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = Find(predicate);
        _dbSet.RemoveRange(entities);
    }

    public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
    {
        return await Get(findOptions).FirstOrDefaultAsync(predicate);
    }

    public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? order = null,
        bool isDescending = false, 
        int? page = null, int? limit = null,
        FindOptions? findOptions = null)
    {
        var entity = Get(findOptions).Where(predicate);
        if (order is not null)
        {
            entity = isDescending ? entity.OrderByDescending(order) : entity.OrderBy(order);
        }

        if (page is not null && limit is not null)
        {
            entity = entity.Skip((page.Value - 1) * limit.Value).Take(limit.Value);
        }

        return entity;
    }

    public IQueryable<TEntity> GetAll(int? page = null, int? limit = null, FindOptions? findOptions = null)
    {
        var entity = Get(findOptions).AsQueryable();

        if (page is not null && limit is not null)
        {
            entity = entity.Skip((page.Value - 1) * limit.Value).Take(limit.Value);
        }

        return entity;
    }

    public void Update(TPartialEntity entity, TEntity originalEntity)
    {
        TEntity entityToUpdate = new();
        foreach (var prop in typeof(TEntity).GetProperties())
        {
            var updatingValue = entity.GetType().GetProperty(prop.Name)?.GetValue(entity);
            var originalValue = prop.GetValue(originalEntity);
            prop.SetValue(entityToUpdate, updatingValue ?? originalValue);
        }

        _dbSet.Update(entityToUpdate);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        if (predicate is null)
            return await _dbSet.CountAsync();
        return await _dbSet.CountAsync(predicate);
    }

    public void Detach(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Detached;
    }

    private DbSet<TEntity> Get(FindOptions? findOptions = null)
    {
        findOptions ??= new FindOptions();
        var entity = _dbSet;
        if (findOptions is { IsAsNoTracking: true, IsIgnoreAutoIncludes: true })
        {
            entity.IgnoreAutoIncludes().AsNoTracking();
        }
        else if (findOptions.IsIgnoreAutoIncludes)
        {
            entity.IgnoreAutoIncludes();
        }
        else if (findOptions.IsAsNoTracking)
        {
            entity.AsNoTracking();
        }

        return entity;
    }
}