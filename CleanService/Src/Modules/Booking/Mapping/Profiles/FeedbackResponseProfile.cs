using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace CleanService.Src.Modules.Booking.Mapping.Profiles;

public class FeedbackResponseProfile : Profile
{
    public FeedbackResponseProfile()
    {
        CreateMap<Feedbacks, CusFeedbackResponseDto>()
            .ConstructUsing(entity => new CusFeedbackResponseDto()
            {
                Id = entity.Id,
                BookingId = entity.BookingId,
                Title = entity.Title,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
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