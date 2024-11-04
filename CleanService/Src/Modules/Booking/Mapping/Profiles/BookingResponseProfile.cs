using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace CleanService.Src.Modules.Booking.Mapping.Profiles;

public class BookingResponseProfile : Profile
{
    public BookingResponseProfile()
    {
        CreateMap<Bookings, BookingResponseDto>()
            .ConstructUsing(entity => new BookingResponseDto
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
                    CustomerFeedback = entity.CustomerFeedback,
                    HelperFeedback = entity.HelperFeedback,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt,
                }
            );
        CreateMap<Users, UserBookingResponse>()
            .ConstructUsing(entity => new UserBookingResponse
            {
                Id = entity.Id,
                Gender = entity.Gender.ToString(),
                FullName = entity.FullName,
                IdentityCard = entity.IdentityCard,
                Address = entity.Address,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            });
        CreateMap<ServiceTypes, ServiceTypeBookingResponse>()
            .ConstructUsing(entity => new ServiceTypeBookingResponse
            {
                Id = entity.Id.ToString(),
                CategoryId = entity.CategoryId.ToString(),
                Name = entity.Name,
                Description = entity.Description,
                BasePrice = entity.BasePrice,
                CreatedAt = entity.CreatedAt
            });
        CreateMap<BookingDetails, BookingDetailsResponse>()
            .ConstructUsing(entity => new BookingDetailsResponse
            {
                Id = entity.Id.ToString(),
                BookingId = entity.BookingId.ToString(),
                DurationPriceId = entity.DurationPriceId.ToString(),
                BedroomCount = entity.BedroomCount,
                BathroomCount = entity.BathroomCount,
                KitchenCount = entity.KitchenCount,
                LivingRoomCount = entity.LivingRoomCount,
                SpecialRequirements = entity.SpecialRequirements,
                CreatedAt = entity.CreatedAt
            });
    }
}