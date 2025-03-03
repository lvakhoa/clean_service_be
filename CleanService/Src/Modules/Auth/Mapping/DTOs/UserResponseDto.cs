using CleanService.Src.Attributes;

namespace CleanService.Src.Modules.Auth.Mapping.DTOs;

public class UserResponseDto
{
    public string Id { get; set; }

    [RoleExpose(Models.Enums.UserType.Admin)]
    public string UserType { get; set; }

    public string? Gender { get; set; }

    public string? ProfilePicture { get; set; }

    public string? IdentityCard { get; set; }

    public string FullName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string Email { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
