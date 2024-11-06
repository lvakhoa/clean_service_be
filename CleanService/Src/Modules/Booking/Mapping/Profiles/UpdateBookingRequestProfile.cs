using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace CleanService.Src.Modules.Booking.Mapping.Profiles;

public class UpdateBookingRequestProfile : Profile
{
    public UpdateBookingRequestProfile()
    {
        CreateMap<UpdateBookingRequestDto, PartialBookings>()
            .ConstructUsing(dto => new PartialBookings
            {
                //Id = Guid.Parse(dto.Id),
                HelperId = dto.HelperId,
                Location = dto.Location,
                ScheduledStartTime = dto.ScheduledStartTime,
                ScheduledEndTime = dto.ScheduledEndTime,
                Status = dto.Status,
                CancellationReason = dto.CancellationReason,
                TotalPrice = dto.TotalPrice,
                PaymentStatus = dto.PaymentStatus,
                HelperRating = dto.HelperRating,
                CustomerFeedback = dto.CustomerFeedback,
                HelperFeedback = dto.HelperFeedback
            });
    }
}