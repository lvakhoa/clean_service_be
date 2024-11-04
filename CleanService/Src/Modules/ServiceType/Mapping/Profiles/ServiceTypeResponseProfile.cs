using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.ServiceType.Mapping.DTOs;

namespace CleanService.Src.Modules.ServiceType.Mapping.Profiles;

public class ServiceTypeResponseProfile : Profile
{
    public ServiceTypeResponseProfile()
    {
        CreateMap<ServiceTypes, ServiceTypeResponseDto>()
            .ConstructUsing(entity => new ServiceTypeResponseDto
            {
                Id = entity.Id.ToString(),
                CategoryId = entity.CategoryId.ToString(),
                Name = entity.Name,
                Description = entity.Description,
                BasePrice = entity.BasePrice,
                CreatedAt = entity.CreatedAt,
                IsActive = entity.IsActive
            });
        CreateMap<ServiceCategories, ServiceCategoryReturn>()
            .ConstructUsing(entity => new ServiceCategoryReturn
            {
                Id = entity.Id.ToString(),
                Name = entity.Name,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                IsActive = entity.IsActive
            });
        CreateMap<RoomPricing, RoomPricingReturn>()
            .ConstructUsing(entity => new RoomPricingReturn
            {
                Id = entity.Id.ToString(),
                ServiceTypeId = entity.ServiceTypeId.ToString(),
                RoomType = entity.RoomType.ToString(),
                RoomCount = entity.RoomCount,
                AdditionalPrice = entity.AdditionalPrice,
                CreatedAt = entity.CreatedAt
            });
        CreateMap<DurationPrice, DurationPriceReturn>()
            .ConstructUsing(entity => new DurationPriceReturn
            {
                Id = entity.Id.ToString(),
                ServiceTypeId = entity.ServiceTypeId.ToString(),
                DurationHours = entity.DurationHours,
                PriceMultiplier = entity.PriceMultiplier,
                CreatedAt = entity.CreatedAt
            });
    }
}