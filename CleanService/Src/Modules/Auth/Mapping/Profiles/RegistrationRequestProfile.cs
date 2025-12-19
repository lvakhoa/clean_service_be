using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
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

        CreateMap<SignUpMobileRequestDto, Users>()
            .ConstructUsing(dto => new Users
            {
                Id = Guid.NewGuid().ToString(),
                Email = dto.Email,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Password = dto.Password,
                UserType = dto.UserType,
            });
    }
}
