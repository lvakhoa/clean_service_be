using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Manage.Mapping.DTOs;

namespace CleanService.Src.Modules.Manage.Mapping.Profiles;

public class ComplaintResponseProfile: Profile
{
    public ComplaintResponseProfile()
    {
        CreateMap<Complaints, ComplaintResponseDto>()
            .ConstructUsing(entity => new ComplaintResponseDto
            {
                Id = entity.Id.ToString(),
                ReportedUserId = entity.ReportedUser.Id,
                ReportedUserName = entity.ReportedUser.FullName,
                ReporterId = entity.ReportedBy.Id,
                ReporterUserName = entity.ReportedBy.FullName,
                Reason = entity.Reason,
                Status = entity.Status.ToString(),
                CreatedAt = entity.CreatedAt,
                ResolvedAt = entity.ResolvedAt
            });
    }
}