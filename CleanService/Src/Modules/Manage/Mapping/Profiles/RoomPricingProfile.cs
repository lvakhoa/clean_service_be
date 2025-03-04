using AutoMapper;

using CleanService.Src.Models;
using CleanService.Src.Models.Enums;
using CleanService.Src.Modules.Manage.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Mapping.DTOs.RoomPricing;

namespace CleanService.Src.Modules.Manage.Mapping.Profiles;

public class RoomPricingProfile : Profile
{
    public RoomPricingProfile()
    {
        //For Response
        CreateMap<RoomPricing, RoomPricingResponseDto>()
            .ConstructUsing(entity => new RoomPricingResponseDto
            {
                Id = entity.Id,
                ServiceTypeId = entity.ServiceTypeId,
                ServiceTypeName = entity.ServiceType.Name.ToString(),
                RoomCount = entity.RoomCount,
                RoomType = entity.RoomType.ToString(),
                AdditionalPrice = entity.AdditionalPrice,
                CreatedAt = entity.CreatedAt,
            });

        //For Update
        CreateMap<UpdateRoomPricingRequestDto, PartialRoomPricing>()
            .ConstructUsing(entity => new PartialRoomPricing
            {
                RoomCount = entity.RoomCount,
                AdditionalPrice = entity.AdditionalPrice,
                ServiceTypeId = entity.ServiceTypeId,
                RoomType = entity.RoomType
            });

        //For Create
        CreateMap<CreateRoomPricingRequestDto, RoomPricing>()
            .ConstructUsing(dto => new RoomPricing
            {
                Id = Guid.NewGuid(),
                ServiceTypeId = Guid.Parse(dto.ServiceTypeId),
                RoomType = dto.RoomType ?? RoomType.Bedroom,
                RoomCount = dto.RoomCount ?? 0,
                AdditionalPrice = dto.AdditionalPrice,
                CreatedAt = DateTime.Now
            });
    }
}
