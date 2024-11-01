using CleanService.Src.Modules.Service.DTOs;
using CleanService.Src.Modules.ServiceType.Mapping.DTOs;

namespace CleanService.Src.Modules.Service.Services;

public interface IServiceTypeService
{
    Task<ServiceTypeReturnDto> CreateService(CreateServiceTypeDto createServiceTypeDto);
    
    Task<ServiceTypeReturnDto?> GetServiceById(Guid id);

    decimal GetPriceById(Guid serviceId);
}