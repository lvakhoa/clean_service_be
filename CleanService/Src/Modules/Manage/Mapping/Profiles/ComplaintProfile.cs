using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Manage.Mapping.DTOs;

namespace CleanService.Src.Modules.Manage.Mapping.Profiles;

public class ComplaintProfile: Profile
{
    public ComplaintProfile()
    {
        //For Response
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
        
        //For Update
        CreateMap<UpdateComplaintRequestDto, PartialComplaints>()
            .ConstructUsing(entity => new PartialComplaints
            {
                Status = entity.Status,
                Reason = entity.Reason,
                Resolution = entity.Resolution
            });
    }
}