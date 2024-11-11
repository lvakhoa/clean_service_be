using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace CleanService.Src.Modules.Booking.Mapping.Profiles;

public class RefundResponseProfile : Profile
{
    public RefundResponseProfile()
    {
        CreateMap<Refunds, RefundResponseDto>()
            .ConstructUsing(entity => new RefundResponseDto()
            {
                Id = entity.Id.ToString(),
                BookingId = entity.BookingId.ToString(),
                Reason = entity.Reason,
                CreatedAt = entity.CreatedAt,
                ResolvedAt = entity.ResolvedAt,
            });
        CreateMap<Bookings, BookingResponse>()
            .ConstructUsing(entity => new BookingResponse()
            {
                Id = entity.Id.ToString(),
                CustomerId = entity.CustomerId,
                HelperId = entity.HelperId,
                ServiceTypeId = entity.ServiceTypeId.ToString(),
                Location = entity.Location,
                ScheduledStartTime = entity.ScheduledStartTime,
                ScheduledEndTime = entity.ScheduledEndTime,
                Status = entity.Status.ToString(),
                CancellationReason = entity.CancellationReason,
                TotalPrice = entity.TotalPrice,
                PaymentStatus = entity.PaymentStatus.ToString(),
                PaymentMethod = entity.PaymentMethod,
                HelperRating = entity.HelperRating,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
            });
    }
}