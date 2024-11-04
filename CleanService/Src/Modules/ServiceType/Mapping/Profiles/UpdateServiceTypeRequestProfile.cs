using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.ServiceType.Mapping.DTOs;

namespace CleanService.Src.Modules.ServiceType.Mapping.Profiles;

public class UpdateServiceTypeRequestProfile : Profile
{
    public UpdateServiceTypeRequestProfile()
    {
        CreateMap<UpdateServiceTypeRequestDto, PartialServiceTypes>()
            .ConstructUsing((dto) => new PartialServiceTypes
            {
                Id = Guid.Parse(dto.Id),
                CategoryId = dto.CategoryId != null ? Guid.Parse(dto.CategoryId) : null,
                Name = dto.Name,
                Description = dto.Description,
                BasePrice = dto.BasePrice ?? 0,
            });
    }
}