using CleanService.Src.Models;

namespace CleanService.Src.Modules.ServiceType.Repositories;

public interface IServiceTypeRepository
{
    public Task<ServiceTypes> CreateService(ServiceTypes createService);
    
    public Task<ServiceTypes?> GetServiceById(Guid id);
}