using System.ComponentModel.DataAnnotations;
using CleanService.Src.Models;

namespace CleanService.Src.Modules.Manage.Mapping.DTOs;

public class AdminUpdateUserRequestDto
{
    public string? ProfilePictureUri { get; set; }
    public IFormFile? ProfilePicture { get; set; }
    
    [EnumDataType(typeof(Gender), ErrorMessage = "Gender type must be Male, Female, or Other")]
    public Gender? Gender { get; set; } = null!;
    
    [MaxLength(150)]
    public string? FullName { get; set; } = null!;
    
    public DateTime? DateOfBirth { get; set; }
    
    public string? IdCardUri { get; set; } = null!;
    
    public IFormFile? IdCard { get; set; }
    
    [MaxLength(255)]
    public string? Address { get; set; } = null!;
    
    [MaxLength(20)]
    // [RegularExpression("/(84|0[3|5|7|8|9])+([0-9]{8})\b/g", ErrorMessage = "Invalid phone number")]
    public string? PhoneNumber { get; set; } = null!;
    
    [EnumDataType(typeof(UserStatus), ErrorMessage = "Invalid user status specified")]
    public UserStatus? UserStatus { get; set; }
    
    [EnumDataType(typeof(UserType), ErrorMessage = "Invalid user type specified")]
    public UserType? UserType { get; set; }
    
    public int? NumberOfViolation { get; set; }
    
    public string? NotificationToken { get; set; }
    
    [MaxLength(255)]
    [EmailAddress]
    public string? Email { get; set; } = null!;
}