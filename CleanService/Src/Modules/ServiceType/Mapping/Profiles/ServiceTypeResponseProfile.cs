using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Service.DTOs;

namespace CleanService.Src.Modules.ServiceType.Mapping.Profiles;

public class ServiceTypeResponseProfile : Profile
{
    public ServiceTypeResponseProfile()
    {
        CreateMap<ServiceTypes, ServiceTypeReturnDto>()
            .ConstructUsing(entity => new ServiceTypeReturnDto
            {
                Id = entity.Id.ToString(),
                CategoryId = entity.CategoryId.ToString(),
                Name = entity.Name,
                Description = entity.Description,
                BasePrice = entity.BasePrice,
                CreatedAt = entity.CreatedAt,
                IsActive = entity.IsActive,
                Category = new ServiceCategoryReturn
                {
                    Id = entity.Category.Id.ToString(),
                    Name = entity.Category.Name,
                    Description = entity.Category.Description,
                    CreatedAt = entity.Category.CreatedAt,
                    IsActive = entity.Category.IsActive
                }
            });
    }
}