using System.ComponentModel.DataAnnotations;
using CleanService.Src.Models;

namespace CleanService.Src.Modules.Auth.Mapping.DTOs;

public class UpdateUserRequestDto
{
    //public string Id { get; set; } = null!;
    
    public string? ProfilePicture { get; set; }
    
    [EnumDataType(typeof(Gender), ErrorMessage = "Gender type must be Male, Female, or Other")]
    public string? Gender { get; set; } = null!;
    
    public string? FullName { get; set; } = null!;
    
    public DateTime? DateOfBirth { get; set; }

    public string? IdentityCard { get; set; } = null!;
    
    public string? Address { get; set; } = null!;
    
    // [RegularExpression("/(84|0[3|5|7|8|9])+([0-9]{8})\b/g", ErrorMessage = "Invalid phone number")]
    public string? PhoneNumber { get; set; } = null!;
}