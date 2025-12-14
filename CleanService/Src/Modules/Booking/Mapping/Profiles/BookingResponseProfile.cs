using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
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
        CreateMap<Helpers, HelperBookingResponse>()
            .ConstructUsing(entity => new HelperBookingResponse
            {
                Id = entity.Id,
                ExperienceDescription = entity.ExperienceDescription,
                ServicesOffered = entity.ServicesOffered != null
                    ? entity.ServicesOffered.Select(s => s.ToString()).ToArray()
                    : null,
                HourlyRate = entity.HourlyRate,
                AverageRating = entity.AverageRating
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
        CreateMap<Refunds, BookingRefundResponse>()
            .ConstructUsing(entity => new BookingRefundResponse
            {
                Id = entity.Id.ToString(),
                BookingId = entity.BookingId.ToString(),
                Reason = entity.Reason,
                Status = entity.Status.ToString(),
                ResolvedAt = entity.ResolvedAt,
                CreatedAt = entity.CreatedAt
            });
    }
}