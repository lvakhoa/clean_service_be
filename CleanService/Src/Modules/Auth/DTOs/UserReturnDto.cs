using CleanService.Src.Models;

namespace CleanService.Src.Modules.Auth.DTOs;

public class UserReturnDto
{
    public string Id { get; set; }
    public string UserType { get; set; }
    public string FullName { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string Email { get; set; }
    
    public string? NotificationToken { get; set; }
    
    public DateTime CreatedAt { get; set; }
}