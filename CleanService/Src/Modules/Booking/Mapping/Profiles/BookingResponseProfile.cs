using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace CleanService.Src.Modules.Booking.Mapping.Profiles;

public class BookingResponseProfile : Profile
{
    public BookingResponseProfile()
    {
        CreateMap<Bookings, BookingReturnDto>()
            .ConstructUsing(entity => new BookingReturnDto
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
        CreateMap<Users, UserBookingReturn>()
            .ConstructUsing(entity => new UserBookingReturn
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
        CreateMap<ServiceTypes, ServiceTypeBookingReturn>()
            .ConstructUsing(entity => new ServiceTypeBookingReturn
            {
                Id = entity.Id.ToString(),
                CategoryId = entity.CategoryId.ToString(),
                Name = entity.Name,
                Description = entity.Description,
                BasePrice = entity.BasePrice,
                CreatedAt = entity.CreatedAt
            });
        CreateMap<BookingDetails, BookingDetailsReturn>()
            .ConstructUsing(entity => new BookingDetailsReturn
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