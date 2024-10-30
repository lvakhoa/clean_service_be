using System.ComponentModel.DataAnnotations;
using CleanService.Src.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanService.Src.Modules.Auth.Mapping.DTOs;

public class UpdateInfoDto
{
    public string? ProfilePicture { get; set; }
    
    public string? Gender { get; set; }
    
    public string? FullName { get; set; }
    
    public DateTime? DateOfBirth { get; set; }

    public string? IdentityCard { get; set; }
    
    public string? Address { get; set; }
    
    [RegularExpression("/(84|0[3|5|7|8|9])+([0-9]{8})\b/g", ErrorMessage = "Invalid phone number")]
    public string? PhoneNumber { get; set; }
}