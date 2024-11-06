using System.Linq.Expressions;

namespace CleanService.Src.Repositories;

public interface IRepository<TEntity, in TPartialEntity> where TEntity : class where TPartialEntity : class
{
    IQueryable<TEntity> GetAll(int? page = null, int? limit = null, FindOptions? findOptions = null);

    Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate, FindOptions? findOptions = null);

    IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>? order = null,bool isDescending = false, int? page = null, int? limit = null,
        FindOptions? findOptions = null);

    Task AddAsync(TEntity entity);

    Task AddManyAsync(IEnumerable<TEntity> entities);

    void Update(TPartialEntity entity, TEntity originalEntity);

    void Delete(TEntity entity);

    void DeleteMany(Expression<Func<TEntity, bool>> predicate);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);

    void Detach(TEntity entity);
}

public class FindOptions
{
    public bool IsIgnoreAutoIncludes { get; set; }
    public bool IsAsNoTracking { get; set; }
}