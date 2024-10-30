using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;

namespace CleanService.Src.Modules.Auth.Mapping.Profiles;

public class HelperResponseProfile : Profile
{
    public HelperResponseProfile()
    {
        CreateMap<Helpers, HelperReturnDto>()
            .ConstructUsing(user => new HelperReturnDto
            {
                Id = user.Id,
                ExperienceDescription = user.ExperienceDescription,
                ResumeUploaded = user.ResumeUploaded,
                ServicesOffered = user.ServicesOffered != null
                    ? user.ServicesOffered.Select(service => service.ToString()).ToArray()
                    : null,
                HourlyRate = user.HourlyRate,
                AverageRating = user.AverageRating,
                CompletedJobs = user.CompletedJobs,
                CancelledJobs = user.CancelledJobs,
            });
    }
}