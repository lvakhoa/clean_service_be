using CleanService.Src.Models.Enums;

namespace CleanService.Src.Modules.Auth.Mapping.DTOs;

public class LogInMobileResponseDto
{
    public string UserId { get; set; } = null!;
    public UserType UserType { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
