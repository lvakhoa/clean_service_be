namespace CleanService.Src.Modules.Auth.Mapping.DTOs;

public class HelperReturnDto
{
    public string Id { get; set; } = null!;
    
    public string? ExperienceDescription { get; set; }
    
    public string? ResumeUploaded { get; set; }
    
    public string[]? ServicesOffered { get; set; }
    
    public decimal HourlyRate { get; set; }
    
    public decimal AverageRating { get; set; } 
    
    public int CompletedJobs { get; set; }
    
    public int CancelledJobs { get; set; }
}