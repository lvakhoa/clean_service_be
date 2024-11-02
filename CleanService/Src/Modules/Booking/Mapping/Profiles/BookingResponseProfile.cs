using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace CleanService.Src.Modules.Booking.Mapping.Profiles;

public class BookingResponseProfile : Profile
{
    public BookingResponseProfile()
    {
        CreateMap<Bookings, BookingReturnDto>()
            .ConstructUsing(user => new BookingReturnDto
                {
                    Id = user.Id.ToString(),
                    CustomerId = user.CustomerId,
                    HelperId = user.HelperId,
                    ServiceTypeId = user.ServiceTypeId.ToString(),
                    Location = user.Location,
                    ScheduledStartTime = user.ScheduledStartTime,
                    ScheduledEndTime = user.ScheduledEndTime,
                    Status = user.Status.ToString(),
                    CancellationReason = user.CancellationReason,
                    TotalPrice = user.TotalPrice,
                    PaymentStatus = user.PaymentStatus.ToString(),
                    PaymentMethod = user.PaymentMethod,
                    HelperRating = user.HelperRating,
                    CustomerFeedback = user.CustomerFeedback,
                    HelperFeedback = user.HelperFeedback,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                    Customer = new UserBookingReturn
                    {
                        Id = user.Customer.Id,
                        Gender = user.Customer.Gender.ToString(),
                        FullName = user.Customer.FullName,
                        IdentityCard = user.Customer.IdentityCard,
                        Address = user.Customer.Address,
                        PhoneNumber = user.Customer.PhoneNumber,
                        Email = user.Customer.Email,
                        CreatedAt = user.Customer.CreatedAt,
                        UpdatedAt = user.Customer.UpdatedAt
                    },
                    Helper = new UserBookingReturn
                    {
                        Id = user.Customer.Id,
                        Gender = user.Customer.Gender.ToString(),
                        FullName = user.Customer.FullName,
                        IdentityCard = user.Customer.IdentityCard,
                        Address = user.Customer.Address,
                        PhoneNumber = user.Customer.PhoneNumber,
                        Email = user.Customer.Email,
                        CreatedAt = user.Customer.CreatedAt,
                        UpdatedAt = user.Customer.UpdatedAt
                    },
                    ServiceType = new ServiceTypeBookingReturn
                    {
                        Id = user.ServiceType.Id.ToString(),
                        CategoryId = user.ServiceType.CategoryId.ToString(),
                        Name = user.ServiceType.Name,
                        Description = user.ServiceType.Description,
                        BasePrice = user.ServiceType.BasePrice,
                        CreatedAt = user.ServiceType.CreatedAt
                    },
                    BookingDetails = new BookingDetailsReturn
                    {
                        Id = user.BookingDetails.Id.ToString(),
                        BookingId = user.BookingDetails.BookingId.ToString(),
                        DurationPriceId = user.BookingDetails.DurationPriceId.ToString(),
                        BedroomCount = user.BookingDetails.BedroomCount,
                        BathroomCount = user.BookingDetails.BathroomCount,
                        KitchenCount = user.BookingDetails.KitchenCount,
                        LivingRoomCount = user.BookingDetails.LivingRoomCount,
                        SpecialRequirements = user.BookingDetails.SpecialRequirements,
                        CreatedAt = user.BookingDetails.CreatedAt
                    }
                }
            );
    }
}