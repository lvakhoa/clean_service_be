using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Manage.Mapping.DTOs;

namespace CleanService.Src.Modules.Manage.Mapping.Profiles;

public class HelperDetailResponseProfile: Profile
{
    public HelperDetailResponseProfile()
    {
        CreateMap<Helpers, HelperDetailResponseDto>()
            .ConstructUsing(entity => new HelperDetailResponseDto
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
                Gender = entity.User.Gender.ToString(),
                ProfilePicture = entity.User.ProfilePicture,
                FullName = entity.User.FullName,
                DateOfBirth = entity.User.DateOfBirth,
                Address = entity.User.Address,
                PhoneNumber = entity.User.PhoneNumber,
                Email = entity.User.Email
            });
    }
}