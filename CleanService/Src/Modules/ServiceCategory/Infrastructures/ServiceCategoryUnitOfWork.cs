using CleanService.Src.Models;
using CleanService.Src.Repositories.ServiceCategory;

namespace CleanService.Src.Modules.ServiceCategory.Infrastructures;

public class ServiceCategoryUnitOfWork : IServiceCategoryUnitOfWork
{    private readonly CleanServiceContext _dbContext;

    public IServiceCategoryRepository ServiceCategoryRepository { get; }

    public ServiceCategoryUnitOfWork(CleanServiceContext dbContext, IServiceCategoryRepository serviceCategoryRepository)
    {
        _dbContext = dbContext;
        ServiceCategoryRepository = serviceCategoryRepository;
    }
    
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}