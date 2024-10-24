using CleanService.Src.Modules.Service.DTOs;
using CleanService.Src.Models;

namespace CleanService.Src.Modules.Service.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly CleanServiceContext _dbContext;

    public ServiceRepository(CleanServiceContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ServiceReturnDto> CreateService(CreateServiceDto createService)
    {
        var serviceEntity = await _dbContext.Services.AddAsync(new Models.Services
        {
                Id = Guid.NewGuid(),
                ServiceName = createService.ServiceName,
                Description = createService.Description,
        });

        await _dbContext.SaveChangesAsync();
        
        return new ServiceReturnDto
        {
            Id = serviceEntity.Entity.Id,
            ServiceName = serviceEntity.Entity.ServiceName,
            Description = serviceEntity.Entity.Description,
        };
    }

    public async Task<ServiceReturnDto?> GetServiceById(Guid id)
    {
        var service = _dbContext.Services.FirstOrDefault(s => s.Id == id);
        
        return service is not null
            ? new ServiceReturnDto
            {
                Id = service.Id,
                ServiceName = service.ServiceName,
                Description = service.Description
            }
            : null;
    }
}