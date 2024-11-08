using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace CleanService.Src.Modules.Booking.Mapping.Profiles;

public class CreateComplaintProfile : Profile
{
    public CreateComplaintProfile()
    {
        CreateMap<CreateComplaintDto, Complaints>()
            .ConstructUsing(dto => new Complaints()
            {
                Id = Guid.NewGuid(),
                BookingId = dto.BookingId,
                ReportedById = dto.ReportedById,
                ReportedUserId = dto.ReportedUserId,
                Reason = dto.Reason,
                Resolution = dto.Resolution ?? string.Empty,
                ResolvedAt = dto.ResolvedAt,
            });
    }
}