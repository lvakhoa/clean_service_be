using CleanService.Src.Modules.Service.DTOs;

namespace CleanService.Src.Modules.Service.Repositories;

public interface IServiceRepository
{
    public Task<ServiceReturnDto> CreateService(CreateServiceDto createService);
    
    public Task<ServiceReturnDto?> GetServiceById(Guid id);
}