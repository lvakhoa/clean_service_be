using System.ComponentModel.DataAnnotations;
using CleanService.Src.Models;

namespace CleanService.Src.Modules.Manage.Mapping.DTOs;

public class AdminUpdateHelperRequestDto
{
    public string? ExperienceDescription { get; set; }

    public string? ResumeUploaded { get; set; }

    public string[]? ServicesOffered { get; set; }

    public decimal? HourlyRate { get; set; }
    
    public decimal? AverageRating { get; set; }
    
    public int? CompletedJobs { get; set; }
    
    public int? CancelledJobs { get; set; }
}