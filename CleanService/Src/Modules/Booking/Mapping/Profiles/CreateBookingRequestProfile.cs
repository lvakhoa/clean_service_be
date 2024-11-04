using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace CleanService.Src.Modules.Booking.Mapping.Profiles;

public class CreateBookingRequestProfile : Profile
{
    public CreateBookingRequestProfile()
    {
        CreateMap<CreateBookingRequestDto, Bookings>()
            .ConstructUsing(dto => new Bookings
            {
                Id = Guid.NewGuid(),
                CustomerId = dto.CustomerId,
                ServiceTypeId = Guid.Parse(dto.ServiceTypeId),
                Location = dto.Location,
                ScheduledStartTime = dto.ScheduledStartTime,
                ScheduledEndTime = dto.ScheduledEndTime,
                PaymentMethod = dto.PaymentMethod
            });
        CreateMap<CreateBookingDetails, BookingDetails>()
            .ConstructUsing(dto => new BookingDetails
            {
                DurationPriceId = Guid.Parse(dto.DurationPriceId),
                BedroomCount = dto.BedroomCount,
                BathroomCount = dto.BathroomCount,
                KitchenCount = dto.KitchenCount,
                LivingRoomCount = dto.LivingRoomCount,
                SpecialRequirements = dto.SpecialRequirements
            });
    }
}