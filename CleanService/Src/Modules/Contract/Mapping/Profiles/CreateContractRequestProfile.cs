using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Contract.Mapping.DTOs;

namespace CleanService.Src.Modules.Contract.Mapping.Profiles;

public class CreateContractRequestProfile : Profile
{
    public CreateContractRequestProfile()
    {
        CreateMap<CreateContractRequestDto, BookingContracts>()
            .ConstructUsing(dto => new BookingContracts
            {
                BookingId = dto.BookingId,
                Content = dto.Content
            });
    }
}