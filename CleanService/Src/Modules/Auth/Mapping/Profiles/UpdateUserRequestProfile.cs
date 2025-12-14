using AutoMapper;

using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
using CleanService.Src.Models.Enums;
using CleanService.Src.Modules.Auth.Mapping.DTOs;

namespace CleanService.Src.Modules.Auth.Mapping.Profiles;

public class UpdateUserRequestProfile : Profile
{
    public UpdateUserRequestProfile()
    {
        CreateMap<UpdateUserRequestDto, PartialUsers>()
            .ConstructUsing(dto => new PartialUsers
            {
                //Id = dto.Id,
                FullName = dto.FullName,
                Gender = dto.Gender != null ? Enum.Parse<Gender>(dto.Gender) : null,
                ProfilePicture = dto.ProfilePicture,
                DateOfBirth = dto.DateOfBirth,
                Address = dto.Address,
                IdentityCard = dto.IdentityCard,
                PhoneNumber = dto.PhoneNumber,
            });
    }
}
