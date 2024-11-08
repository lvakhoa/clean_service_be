using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.Profiles;
using CleanService.Src.Modules.Manage.Mapping.DTOs;

namespace CleanService.Src.Modules.Manage.Mapping.Profiles;

public class UpdateComplaintRequestProfile: Profile
{
    public UpdateComplaintRequestProfile()
    {
        CreateMap<UpdateComplaintRequestDto, PartialComplaints>()
            .ConstructUsing(entity => new PartialComplaints
            {
                Status = entity.Status,
                Reason = entity.Reason,
                Resolution = entity.Resolution
            });
    }
}