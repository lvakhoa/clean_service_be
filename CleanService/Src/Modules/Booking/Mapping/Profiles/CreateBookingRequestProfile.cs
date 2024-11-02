using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace CleanService.Src.Modules.Booking.Mapping.Profiles;

public class CreateBookingRequestProfile : Profile
{
    public CreateBookingRequestProfile()
    {
        CreateMap<CreateBookingDto, Bookings>()
            .ConstructUsing(user => new Bookings
            {
                CustomerId = user.CustomerId,
                ServiceTypeId = Guid.Parse(user.ServiceTypeId),
                Location = user.Location,
                ScheduledStartTime = user.ScheduledStartTime,
                ScheduledEndTime = user.ScheduledEndTime,
                PaymentMethod = user.PaymentMethod,
                BookingDetails = new BookingDetails
                {
                    DurationPriceId = Guid.Parse(user.BookingDetails.DurationPriceId),
                    BedroomCount = user.BookingDetails.BedroomCount,
                    BathroomCount = user.BookingDetails.BathroomCount,
                    KitchenCount = user.BookingDetails.KitchenCount,
                    LivingRoomCount = user.BookingDetails.LivingRoomCount,
                    SpecialRequirements = user.BookingDetails.SpecialRequirements
                }
            });
    }
}