using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Manage.Mapping.DTOs;

namespace CleanService.Src.Modules.Manage.Mapping.Profiles;

public class AdminUpdateHelperRequestProfile: Profile
{
    public AdminUpdateHelperRequestProfile()
    {
        CreateMap<AdminUpdateHelperRequestDto, PartialHelper>()
            .ConstructUsing(dto => new PartialHelper()
            {
                ExperienceDescription = dto.ExperienceDescription,
                ResumeUploaded = dto.ResumeUploaded,
                ServicesOffered = dto.ServicesOffered != null
                    ? dto.ServicesOffered.Select(Guid.Parse).ToArray()
                    : null,
                HourlyRate = dto.HourlyRate,
                AverageRating = dto.AverageRating,
                CompletedJobs = dto.CompletedJobs,
                CancelledJobs = dto.CancelledJobs,
            });
    }
}