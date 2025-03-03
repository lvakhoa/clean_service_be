using CleanService.Src.Common;

namespace CleanService.Src.Infrastructures.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();

    Task RollBackChangesAsync();

    IBaseRepository<T, TI> Repository<T, TI>() where T : BaseEntity, new() where TI : BaseEntity;
}
