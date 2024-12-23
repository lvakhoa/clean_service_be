using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.ServiceCategory.Mapping.DTOs;
using CleanService.Src.Modules.ServiceType.Mapping.DTOs;

namespace CleanService.Src.Modules.ServiceCategory.Mapping.Profiles;

public class UpdateServiceCategoryRequestProfile : Profile
{
    public UpdateServiceCategoryRequestProfile()
    {
        CreateMap<UpdateServiceCategoryRequestDto, PartialServiceCategories>()
            .ConstructUsing((dto) => new PartialServiceCategories
            {
                Name = dto.Name,
                Description = dto.Description,
                IsActive = dto.IsActive
            });
    }
}