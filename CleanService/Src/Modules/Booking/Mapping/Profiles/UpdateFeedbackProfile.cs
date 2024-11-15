using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace CleanService.Src.Modules.Booking.Mapping.Profiles;

public class UpdateFeedbackProfile : Profile
{
    public UpdateFeedbackProfile()
    {
        CreateMap<UpdateFeedbackDto, PartialFeedback>()
            .ConstructUsing(dto => new PartialFeedback
            {
                BookingId = dto.BookingId,
                Title = dto.Title,
                Description = dto.Description,
            });
    }
}