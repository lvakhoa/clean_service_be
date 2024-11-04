using CleanService.Src.Modules.ServiceType.Mapping.DTOs;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.ServiceType.Services;

public interface IServiceTypeService
{
    Task CreateServiceType(CreateServiceTypeRequestDto createServiceTypeDto);
    
    Task UpdateServiceType(Guid id, UpdateServiceTypeRequestDto updateServiceTypeDto);
    
    Task<ServiceTypeResponseDto?> GetServiceTypeById(Guid id);
    
    Task<Pagination<ServiceTypeResponseDto>> GetServiceTypes(int? page, int? limit);
}