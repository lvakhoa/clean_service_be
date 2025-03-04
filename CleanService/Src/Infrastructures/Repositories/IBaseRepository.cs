using CleanService.Src.Common;
using CleanService.Src.Infrastructures.Specifications;

namespace CleanService.Src.Infrastructures.Repositories;

public interface IBaseRepository<TEntity, TPartialEntity>
    where TEntity : BaseEntity, new() where TPartialEntity : BaseEntity
{
    Task<TEntity> GetFirstOrThrowAsync(ISpecification<TEntity>? spec = null);

    Task<TEntity?> GetFirstAsync(ISpecification<TEntity>? spec = null);

    Task<List<TEntity>> GetAllAsync(ISpecification<TEntity>? spec = null);

    Task<int> CountAsync(ISpecification<TEntity>? spec = null);

    Task<bool> AnyAsync(ISpecification<TEntity>? spec = null);

    Task<TEntity> AddAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TPartialEntity partialEntity, TEntity entity);

    Task<TEntity> DeleteAsync(TEntity entity);

    Task Detach(TEntity entity);

}
