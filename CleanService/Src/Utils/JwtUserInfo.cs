using CleanService.Src.Models.Enums;

namespace CleanService.Src.Utils;

public class JwtUserInfo
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public UserType UserType { get; set; }
    public string? PhoneNumber { get; set; }
}
