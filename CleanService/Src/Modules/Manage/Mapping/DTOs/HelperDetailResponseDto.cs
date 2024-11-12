namespace CleanService.Src.Modules.Manage.Mapping.DTOs;

public class HelperDetailResponseDto
{
    public string Id { get; set; } = null!;
    
    public string? ExperienceDescription { get; set; }
    
    public string? ResumeUploaded { get; set; }
    
    public string[]? ServicesOffered { get; set; }
    
    public decimal HourlyRate { get; set; }
    
    public decimal AverageRating { get; set; } 
    
    public int CompletedJobs { get; set; }
    
    public int CancelledJobs { get; set; }
    
    public string? Gender { get; set; }
    
    public string? ProfilePicture { get; set; }
    
    public string FullName { get; set; }
    
    public DateTime? DateOfBirth { get; set; }

    public string? Address { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public string Email { get; set; }
}