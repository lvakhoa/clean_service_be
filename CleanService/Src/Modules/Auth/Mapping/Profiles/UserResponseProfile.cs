using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;

namespace CleanService.Src.Modules.Auth.Mapping.Profiles;

public class UserResponseProfile : Profile
{
    public UserResponseProfile()
    {
        CreateMap<Users, UserReturnDto>()
            .ConstructUsing(user => new UserReturnDto
            {
                Id = user.Id,
                FullName = user.FullName,
                UserType = user.UserType.ToString(),
                Gender = user.Gender.ToString(),
                ProfilePicture = user.ProfilePicture,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            });
    }
}