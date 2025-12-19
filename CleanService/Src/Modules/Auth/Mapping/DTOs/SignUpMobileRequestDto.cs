using CleanService.Src.Models.Enums;

namespace CleanService.Src.Modules.Auth.Mapping.DTOs;

public class SignUpMobileRequestDto
{
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserType UserType { get; set; }
}
