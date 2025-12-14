using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace CleanService.Src.Modules.Booking.Mapping.Profiles;

public class UpdateRefundProfile : Profile
{
    public UpdateRefundProfile()
    {
        CreateMap<CusUpdateRefundRequestDto, PartialRefunds>()
            .ConstructUsing(dto => new PartialRefunds()
            {
                BookingId = dto.BookingId,
                Reason = dto.Reason,
                Status = dto.Status,
                ResolvedAt = dto.ResolvedAt,
            });
    }
}