using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;

namespace CleanService.Src.Modules.Auth.Mapping.Profiles;

public class RegistrationRequestProfile : Profile
{
    public RegistrationRequestProfile()
    {
        CreateMap<RegistrationDto, Users>()
            .ConstructUsing(user => new Users
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.Fullname,
                UserType = user.UserType,
            });
    }
}