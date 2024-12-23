using CleanService.Src.Modules.ServiceCategory.Mapping.DTOs;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.ServiceCategory.Services;

public interface IServiceCategoryService
{
    Task CreateServiceCategory(CreateServiceCategoryRequestDto createServiceCategoryDto);
    
    Task UpdateServiceCategory(Guid id, UpdateServiceCategoryRequestDto updateServiceCategoryDto);
    
    Task<ServiceCategoryResponseDto?> GetServiceCategoryById(Guid id);
    
    Task<Pagination<ServiceCategoryResponseDto>> GetServiceCategories(int? page, int? limit);
}