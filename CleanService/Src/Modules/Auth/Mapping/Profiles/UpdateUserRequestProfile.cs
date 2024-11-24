using AutoMapper;
using CleanService.Src.Models;
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
                ProfilePicture = dto.ProfilePictureUri,
                DateOfBirth = dto.DateOfBirth,
                Address = dto.Address,
                IdentityCard = dto.IdentityCardUri,
                PhoneNumber = dto.PhoneNumber,
            });
    }
}