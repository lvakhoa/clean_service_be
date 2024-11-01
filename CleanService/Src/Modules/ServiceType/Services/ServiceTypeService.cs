using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Service.DTOs;
using CleanService.Src.Modules.Service.Services;
using CleanService.Src.Modules.ServiceType.Mapping.DTOs;
using CleanService.Src.Modules.ServiceType.Repositories;

namespace CleanService.Src.Modules.ServiceType.Services;

public class ServiceTypeService : IServiceTypeService
{
    private readonly IServiceTypeRepository _serviceTypeRepository;
    private readonly IMapper _mapper;

    public ServiceTypeService(IServiceTypeRepository serviceTypeRepository, IMapper mapper)
    {
        _serviceTypeRepository = serviceTypeRepository;
        _mapper = mapper;
    }
    
    public async Task<ServiceTypeReturnDto> CreateService(CreateServiceTypeDto createServiceTypeDto)
    {
        var serviceType = _mapper.Map<ServiceTypes>(createServiceTypeDto);
        var serviceTypeEntity = await _serviceTypeRepository.CreateService(serviceType);
        return _mapper.Map<ServiceTypeReturnDto>(serviceTypeEntity);
    }

    public async Task<ServiceTypeReturnDto?> GetServiceById(Guid id)
    {
        var serviceTypeEntity = await _serviceTypeRepository.GetServiceById(id);
        return _mapper.Map<ServiceTypeReturnDto>(serviceTypeEntity);
    }

    public decimal GetPriceById(Guid serviceId)
    {
        var service = _serviceTypeRepository.GetServiceById(serviceId);
        return 1;
    }
}