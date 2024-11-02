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
                },
                RoomPricing = entity.RoomPricing.Select(roomPricing => new RoomPricingReturn
                {
                    Id = roomPricing.Id.ToString(),
                    ServiceTypeId = roomPricing.ServiceTypeId.ToString(),
                    RoomType = roomPricing.RoomType.ToString(),
                    RoomCount = roomPricing.RoomCount,
                    AdditionalPrice = roomPricing.AdditionalPrice,
                    CreatedAt = roomPricing.CreatedAt
                }).ToList(),
                DurationPrice = entity.DurationPrice.Select(durationPrice => new DurationPriceReturn
                {
                    Id = durationPrice.Id.ToString(),
                    ServiceTypeId = durationPrice.ServiceTypeId.ToString(),
                    DurationHours = durationPrice.DurationHours,
                    PriceMultiplier = durationPrice.PriceMultiplier,
                    CreatedAt = durationPrice.CreatedAt
                }).ToList()
            });
    }
}