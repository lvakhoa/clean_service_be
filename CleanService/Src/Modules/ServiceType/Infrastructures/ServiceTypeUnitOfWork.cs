using CleanService.Src.Models;
using CleanService.Src.Repositories.ServiceCategory;
using CleanService.Src.Repositories.ServiceType;

namespace CleanService.Src.Modules.ServiceType.Infrastructures;

public class ServiceTypeUnitOfWork : IServiceTypeUnitOfWork
{
    private readonly CleanServiceContext _dbContext;

    public IServiceTypeRepository ServiceTypeRepository { get; }

    public IServiceCategoryRepository ServiceCategoryRepository { get; }

    public ServiceTypeUnitOfWork(CleanServiceContext dbContext, IServiceCategoryRepository serviceCategoryRepository,
        IServiceTypeRepository serviceTypeRepository)
    {
        _dbContext = dbContext;
        ServiceCategoryRepository = serviceCategoryRepository;
        ServiceTypeRepository = serviceTypeRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}