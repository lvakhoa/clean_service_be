using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Manage.Mapping.DTOs;

namespace CleanService.Src.Modules.Manage.Mapping.Profiles;

public class AdminUpdateUserRequestProfile:Profile
{
    public AdminUpdateUserRequestProfile()
    {
        CreateMap<AdminUpdateUserRequestDto, PartialUsers>()
            .ConstructUsing(dto => new PartialUsers()
            {
                ProfilePicture = dto.ProfilePicture,
                Gender = dto.Gender,
                FullName = dto.FullName,
                DateOfBirth = dto.DateOfBirth,
                IdentityCard = dto.IdentityCard,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                Status = dto.UserStatus,
                UserType  = dto.UserType,
                NumberOfViolation = dto.NumberOfViolation ?? 0,
                NotificationToken = dto.NotificationToken,
                Email = dto.Email
            });
    }
}