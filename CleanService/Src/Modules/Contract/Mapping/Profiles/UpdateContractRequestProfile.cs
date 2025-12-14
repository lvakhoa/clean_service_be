using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
using CleanService.Src.Modules.Contract.Mapping.DTOs;

namespace CleanService.Src.Modules.Contract.Mapping.Profiles;

public class UpdateContractRequestProfile : Profile
{
    public UpdateContractRequestProfile()
    {
        CreateMap<UpdateContractRequestDto, PartialBookingContracts>()
            .ConstructUsing(dto => new PartialBookingContracts
            {
                //Id = Guid.Parse(dto.Id),
                BookingId = dto.BookingId != null ? Guid.Parse(dto.BookingId) : null,
                Content = dto.Content
            });
    }
}