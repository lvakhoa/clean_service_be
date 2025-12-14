using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
using CleanService.Src.Modules.Auth.Mapping.DTOs;

namespace CleanService.Src.Modules.Auth.Mapping.Profiles;

public class HelperResponseProfile : Profile
{
    public HelperResponseProfile()
    {
        CreateMap<Helpers, HelperResponseDto>()
            .ConstructUsing(entity => new HelperResponseDto
            {
                Id = entity.Id,
                ExperienceDescription = entity.ExperienceDescription,
                ResumeUploaded = entity.ResumeUploaded,
                ServicesOffered = entity.ServicesOffered != null
                    ? entity.ServicesOffered.Select(service => service.ToString()).ToArray()
                    : null,
                HourlyRate = entity.HourlyRate,
                AverageRating = entity.AverageRating,
                CompletedJobs = entity.CompletedJobs,
                CancelledJobs = entity.CancelledJobs,
            });
    }
}