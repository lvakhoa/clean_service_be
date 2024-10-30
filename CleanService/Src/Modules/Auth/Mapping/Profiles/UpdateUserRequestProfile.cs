using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;

namespace CleanService.Src.Modules.Auth.Mapping.Profiles;

public class UpdateUserRequestProfile : Profile
{
    public UpdateUserRequestProfile()
    {
        CreateMap<UpdateInfoDto, PartialUsers>()
            .ConstructUsing(user => new PartialUsers
            {
                FullName = user.FullName,
                Gender = user.Gender != null ? Enum.Parse<Gender>(user.Gender) : null,
                ProfilePicture = user.ProfilePicture,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address,
                IdentityCard = user.IdentityCard,
                PhoneNumber = user.PhoneNumber,
            });
    }
}