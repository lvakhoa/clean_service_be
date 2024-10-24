using CleanService.Src.Modules.Service.DTOs;

namespace CleanService.Src.Modules.Service.Services;

public interface IServiceService
{
    Task<ServiceReturnDto> CreateService(CreateServiceDto createServiceDto);
    
    Task<ServiceReturnDto?> GetServiceById(Guid id);
}