using CleanService.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.ServiceType.Repositories;

public class ServiceTypeRepository : IServiceTypeRepository
{
    private readonly CleanServiceContext _dbContext;

    public ServiceTypeRepository(CleanServiceContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceTypes> CreateService(ServiceTypes createService)
    {
        var serviceEntity = await _dbContext.ServiceTypes.AddAsync(createService);

        await _dbContext.SaveChangesAsync();

        return serviceEntity.Entity;
    }

    public async Task<ServiceTypes?> GetServiceById(Guid id)
    {
        return await _dbContext.ServiceTypes.FirstOrDefaultAsync(s => s.Id == id);
    }
}