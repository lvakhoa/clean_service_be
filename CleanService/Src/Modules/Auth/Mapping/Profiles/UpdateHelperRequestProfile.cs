using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;

namespace CleanService.Src.Modules.Auth.Mapping.Profiles;

public class UpdateHelperRequestProfile : Profile
{
    public UpdateHelperRequestProfile()
    {
        CreateMap<UpdateHelperDto, PartialHelper>()
            .ConstructUsing(user => new PartialHelper
            {
                ExperienceDescription = user.ExperienceDescription,
                ResumeUploaded = user.ResumeUploaded,
                ServicesOffered = user.ServicesOffered != null
                    ? user.ServicesOffered.Select(Guid.Parse).ToArray()
                    : null,
            });
    }
}