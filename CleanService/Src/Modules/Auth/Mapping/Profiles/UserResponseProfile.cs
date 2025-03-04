using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;

namespace CleanService.Src.Modules.Auth.Mapping.Profiles;

public class UserResponseProfile : Profile
{
    public UserResponseProfile()
    {
        CreateMap<Users, UserResponseDto>()
            .ConstructUsing(entity => new UserResponseDto
            {
                Id = entity.Id,
                FullName = entity.FullName,
                UserType = entity.UserType.ToString(),
                Gender = entity.Gender.ToString(),
                ProfilePicture = entity.ProfilePicture,
                DateOfBirth = entity.DateOfBirth,
                Address = entity.Address,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            });
    }
}