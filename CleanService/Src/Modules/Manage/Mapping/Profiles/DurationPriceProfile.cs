using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
using CleanService.Src.Modules.Manage.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Mapping.DTOs.DurationPrice;

namespace CleanService.Src.Modules.Manage.Mapping.Profiles;

public class DurationPriceProfile: Profile
{
    public DurationPriceProfile()
    {
        //For Response
        CreateMap<DurationPrice, DurationPricingResponseDto>()
            .ConstructUsing(entity => new DurationPricingResponseDto
            {
                Id = entity.Id.ToString(),
                ServiceTypeId = entity.ServiceTypeId.ToString(),
                ServiceTypeName = entity.ServiceType.Name,
                DurationHours = entity.DurationHours,
                PriceMultiplier = entity.PriceMultiplier
            });
        
        //For Create
        CreateMap<CreateDurationPriceRequestDto, DurationPrice>()
            .ConstructUsing(entity => new DurationPrice
            {
                Id = Guid.NewGuid(),
                PriceMultiplier = entity.PriceMultiplier,
                ServiceTypeId = entity.ServiceTypeId,
                DurationHours = entity.DurationHours,
            });
        
        //For Update
        CreateMap<UpdateDurationPriceRequestDto, PartialDurationPrice>()
            .ConstructUsing(entity => new PartialDurationPrice
            {
                ServiceTypeId = entity.ServiceTypeId,
                DurationHours = entity.DurationHours,
                PriceMultiplier = entity.PriceMultiplier
            });
    }
}