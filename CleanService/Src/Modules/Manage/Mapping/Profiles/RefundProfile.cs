using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
using CleanService.Src.Modules.Manage.Mapping.DTOs;
using CleanService.Src.Modules.Manage.Mapping.DTOs.Refund;

namespace CleanService.Src.Modules.Manage.Mapping.Profiles;

public class RefundProfile: Profile
{
    public RefundProfile()
    {
        //For Response
        CreateMap<Refunds, RefundResponseDto>()
            .ConstructUsing(entity => new RefundResponseDto
            {
                Id = entity.Id.ToString(),
                HelperId = entity.Booking.HelperId,
                HelperName = entity.Booking.Helper!.User.FullName,
                CustomerId = entity.Booking.CustomerId,
                CustomerName = entity.Booking.Customer.FullName,
                Reason = entity.Reason,
                Status = entity.Status.ToString(),
                CreatedAt = entity.CreatedAt,
                ResolvedAt = entity.ResolvedAt
            });
        
        //For Update
        CreateMap<UpdateRefundRequestDto, PartialRefunds>()
            .ConstructUsing(entity => new PartialRefunds
            {
                BookingId = Guid.Parse(entity.BookingId),
                Status = entity.Status,
                Reason = entity.Reason,
                ResolvedAt = entity.ResolvedAt
            });
    }
}