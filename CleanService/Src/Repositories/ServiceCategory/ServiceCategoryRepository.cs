using CleanService.Src.Models;

namespace CleanService.Src.Repositories.ServiceCategory;

public class ServiceCategoryRepository : Repository<ServiceCategories>, IServiceCategoryRepository
{
    private readonly CleanServiceContext _dbContext;
    
    public ServiceCategoryRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}