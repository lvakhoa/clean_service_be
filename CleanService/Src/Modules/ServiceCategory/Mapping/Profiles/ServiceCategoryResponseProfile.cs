using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.ServiceCategory.Mapping.DTOs;
using CleanService.Src.Modules.ServiceType.Mapping.DTOs;

namespace CleanService.Src.Modules.ServiceCategory.Mapping.Profiles;

public class ServiceCategoryResponseProfile : Profile
{
    public ServiceCategoryResponseProfile()
    {
        CreateMap<ServiceCategories, ServiceCategoryResponseDto>()
            .ConstructUsing(entity => new ServiceCategoryResponseDto
            {
                Id = entity.Id.ToString(),
                Name = entity.Name,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                IsActive = entity.IsActive
            });
    }
}