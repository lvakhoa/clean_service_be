using System.Linq.Expressions;
using CleanService.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly CleanServiceContext _dbContext;
    
    public Repository(CleanServiceContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Add(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
        _dbContext.SaveChanges();
    }
    
    public void AddMany(IEnumerable<TEntity> entities)
    {
        _dbContext.Set<TEntity>().AddRange(entities);
        _dbContext.SaveChanges();
    }
    
    public void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        _dbContext.SaveChanges();
    }
    
    public void DeleteMany(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = Find(predicate);
        _dbContext.Set<TEntity>().RemoveRange(entities);
        _dbContext.SaveChanges();
    }
    
    public TEntity FindOne(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
    {
        return Get(findOptions).FirstOrDefault(predicate)!;
    }
    
    public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null)
    {
        return Get(findOptions).Where(predicate);
    }
    
    public IQueryable<TEntity> GetAll(FindOptions? findOptions = null)
    {
        return Get(findOptions);
    }
    
    public void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
        _dbContext.SaveChanges();
    }
    
    public bool Any(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbContext.Set<TEntity>().Any(predicate);
    }
    
    public int Count(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbContext.Set<TEntity>().Count(predicate);
    }
    
    private DbSet<TEntity> Get(FindOptions? findOptions = null)
    {
        findOptions ??= new FindOptions();
        var entity = _dbContext.Set<TEntity>();
        if (findOptions.IsAsNoTracking && findOptions.IsIgnoreAutoIncludes)
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