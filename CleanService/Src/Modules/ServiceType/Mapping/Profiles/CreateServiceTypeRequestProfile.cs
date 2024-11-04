using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.ServiceType.Mapping.DTOs;

namespace CleanService.Src.Modules.ServiceType.Mapping.Profiles;

public class CreateServiceTypeRequestProfile : Profile
{
    public CreateServiceTypeRequestProfile()
    {
        CreateMap<CreateServiceTypeRequestDto, ServiceTypes>()
            .ConstructUsing((dto) => new ServiceTypes
            {
                CategoryId = Guid.Parse(dto.CategoryId),
                Name = dto.Name,
                Description = dto.Description,
                BasePrice = dto.BasePrice
            });
    }
}