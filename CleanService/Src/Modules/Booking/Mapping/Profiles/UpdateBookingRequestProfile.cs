using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace CleanService.Src.Modules.Booking.Mapping.Profiles;

public class UpdateBookingRequestProfile : Profile
{
    public UpdateBookingRequestProfile()
    {
        CreateMap<UpdateBookingDto, PartialBookings>()
            .ConstructUsing(user => new PartialBookings
            {
                HelperId = user.HelperId,
                Location = user.Location,
                ScheduledStartTime = user.ScheduledStartTime,
                ScheduledEndTime = user.ScheduledEndTime,
                Status = user.Status,
                CancellationReason = user.CancellationReason,
                TotalPrice = user.TotalPrice,
                PaymentStatus = user.PaymentStatus,
                HelperRating = user.HelperRating,
                CustomerFeedback = user.CustomerFeedback,
                HelperFeedback = user.HelperFeedback
            });
    }
}