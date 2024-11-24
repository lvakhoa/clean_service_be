using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;

namespace CleanService.Src.Modules.Auth.Mapping.Profiles;

public class UpdateHelperRequestProfile : Profile
{
    public UpdateHelperRequestProfile()
    {
        CreateMap<UpdateHelperRequestDto, PartialHelper>()
            .ConstructUsing(dto => new PartialHelper
            {
                //Id = dto.Id,
                ExperienceDescription = dto.ExperienceDescription,
                ResumeUploaded = dto.ResumeUploadeUri,
                ServicesOffered = dto.ServicesOffered != null
                    ? dto.ServicesOffered.Select(Guid.Parse).ToArray()
                    : null,
                HourlyRate = dto.HourlyRate,
            });
    }
}