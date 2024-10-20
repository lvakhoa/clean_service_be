using System.ComponentModel.DataAnnotations;
using CleanService.Src.Models;

namespace CleanService.Src.Modules.Auth.DTOs;

public class UpdateInfoDto
{
    public string? FullName { get; set; }
    
    public string? Address { get; set; }
    
    [RegularExpression("/(84|0[3|5|7|8|9])+([0-9]{8})\\b/g")]
    public string? PhoneNumber { get; set; }
    
    public UserStatus? Status { get; set; }
}