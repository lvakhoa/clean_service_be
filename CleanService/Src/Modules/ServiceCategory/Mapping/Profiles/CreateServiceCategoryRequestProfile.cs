using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.ServiceCategory.Mapping.DTOs;
using CleanService.Src.Modules.ServiceType.Mapping.DTOs;

namespace CleanService.Src.Modules.ServiceCategory.Mapping.Profiles;

public class CreateServiceCategoryRequestProfile : Profile
{
    public CreateServiceCategoryRequestProfile()
    {
        CreateMap<CreateServiceCategoryRequestDto, ServiceCategories>()
            .ConstructUsing((dto) => new ServiceCategories()
            {
                Name = dto.Name,
                Description = dto.Description,
            });
    }
}