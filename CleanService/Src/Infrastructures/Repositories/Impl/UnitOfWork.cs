using CleanService.Src.Common;
using CleanService.Src.Models;

namespace CleanService.Src.Infrastructures.Repositories.Impl;

public class UnitOfWork : IUnitOfWork
{
    private readonly CleanServiceContext _dbContext;
    private readonly IDictionary<Type, dynamic> _repositories;

    public UnitOfWork(CleanServiceContext dbContext)
    {
        _dbContext = dbContext;
        _repositories = new Dictionary<Type, dynamic>();
    }

    public IBaseRepository<T, TI> Repository<T, TI>() where T : BaseEntity, new() where TI : BaseEntity
    {
        var entityType = typeof(T);

        if (_repositories.ContainsKey(entityType))
        {
            return _repositories[entityType];
        }

        var repositoryType = typeof(BaseRepository<,>);

        var repository = Activator.CreateInstance(repositoryType.MakeGenericType(entityType), _dbContext);

        if (repository == null)
        {
            throw new NullReferenceException("Repository should not be null");
        }

        _repositories.Add(entityType, repository);

        return (IBaseRepository<T, TI>) repository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task RollBackChangesAsync()
    {
        await _dbContext.Database.RollbackTransactionAsync();
    }
}
