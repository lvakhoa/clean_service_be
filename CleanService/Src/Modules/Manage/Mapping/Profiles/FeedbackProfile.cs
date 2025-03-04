using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Manage.Mapping.DTOs;

namespace CleanService.Src.Modules.Manage.Mapping.Profiles;

public class FeedbackProfile: Profile
{
    public FeedbackProfile()
    {
        CreateMap<Feedbacks, FeedbackResponseDto>()
            .ConstructUsing(entity => new FeedbackResponseDto
            {
                Id = entity.Id,
                BookingId = entity.BookingId,
                Title = entity.Title,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                HelperRating = entity.Booking.HelperRating,
                CustomerAvatar = entity.Booking.Customer.ProfilePicture,
                CustomerName = entity.Booking.Customer.FullName
            });
    }
}