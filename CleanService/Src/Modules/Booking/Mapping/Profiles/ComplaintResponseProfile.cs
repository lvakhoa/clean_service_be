using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace CleanService.Src.Modules.Booking.Mapping.Profiles;

public class ComplaintResponseProfile : Profile
{
    public ComplaintResponseProfile()
    {
        CreateMap<Complaints, ComplaintResponseDto>()
            .ConstructUsing(entity => new ComplaintResponseDto()
            {
                Id = entity.Id.ToString(),
                BookingId = entity.BookingId.ToString(),
                ReportedById = entity.ReportedById,
                ReportedUserId = entity.ReportedUserId,
                Reason = entity.Reason,
                Resolution = entity.Resolution,
                CreatedAt = entity.CreatedAt,
                ResolvedAt = entity.ResolvedAt,
            });
        CreateMap<Users, UserReportedResponse>()
            .ConstructUsing(entity => new UserReportedResponse()
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
                CustomerFeedback = entity.CustomerFeedback,
                HelperFeedback = entity.HelperFeedback,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            });
    }
}