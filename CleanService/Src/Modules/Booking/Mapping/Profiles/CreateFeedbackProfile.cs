using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace CleanService.Src.Modules.Booking.Mapping.Profiles;

public class CreateFeedbackProfile : Profile
{
    public CreateFeedbackProfile()
    {
        CreateMap<CreateFeedbackDto, Feedbacks>()
            .ConstructUsing(dto => new Feedbacks()
            {
                Id = Guid.NewGuid(),
                BookingId = dto.BookingId,
                Title = dto.Title,
                Description = dto.Description
            });
    }
}