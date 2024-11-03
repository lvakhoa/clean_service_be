using CleanService.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Repositories.ServiceType;

public class ServiceTypeRepository : Repository<ServiceTypes>, IServiceTypeRepository
{
    private readonly CleanServiceContext _dbContext;

    public ServiceTypeRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}