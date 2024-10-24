using CleanService.Src.Modules.Service.DTOs;
using CleanService.Src.Modules.Service.Repositories;

namespace CleanService.Src.Modules.Service.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;

    public ServiceService(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }
    
    public async Task<ServiceReturnDto> CreateService(CreateServiceDto createServiceDto)
    {
        return await _serviceRepository.CreateService(createServiceDto);;
    }

    public async Task<ServiceReturnDto> GetServiceById(Guid id)
    {
        return await _serviceRepository.GetServiceById(id);
    }
}