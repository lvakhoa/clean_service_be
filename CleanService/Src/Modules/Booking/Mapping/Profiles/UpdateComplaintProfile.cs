using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace CleanService.Src.Modules.Booking.Mapping.Profiles;

public class UpdateComplaintProfile : Profile
{
    public UpdateComplaintProfile()
    {
        CreateMap<UpdateComplaintDto, PartialComplaints>()
            .ConstructUsing(dto => new PartialComplaints
            {
                BookingId = dto.BookingId,
                ReportedById = dto.ReportedById,
                ReportedUserId = dto.ReportedUserId,
                Reason = dto.Reason,
                Status = dto.Status,
                Resolution = dto.Resolution,
                ResolvedAt = dto.ResolvedAt,
            });
    }
}