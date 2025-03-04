using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;

namespace CleanService.Src.Modules.Auth.Mapping.Profiles;

public class RegistrationRequestProfile : Profile
{
    public RegistrationRequestProfile()
    {
        CreateMap<RegistrationRequestDto, Users>()
            .ConstructUsing(dto => new Users
            {
                Id = dto.Id,
                Email = dto.Email,
                FullName = dto.Fullname,
                UserType = dto.UserType,
            });
    }
}