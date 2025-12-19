using CleanService.Src.Models.Enums;

namespace CleanService.Src.Modules.Auth.Mapping.DTOs;

public class LogInMobileRequestDto
{
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
}
