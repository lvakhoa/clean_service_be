using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace CleanService.Src.Modules.Booking.Mapping.Profiles;

public class CreateRefundProfile : Profile
{
    public CreateRefundProfile()
    {
        CreateMap<CreateRefundRequestDto, Refunds>()
            .ConstructUsing(dto => new Refunds()
            {
                Id = Guid.NewGuid(),
                BookingId = dto.BookingId,
                Reason = dto.Reason,
               // ResolvedAt = dto.ResolvedAt,
            });
    }
}