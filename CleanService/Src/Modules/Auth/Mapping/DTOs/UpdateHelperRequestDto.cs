using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Auth.Mapping.DTOs;

public class UpdateHelperRequestDto
{
    public string Id { get; set; } = null!;
    
    public string? ExperienceDescription { get; set; }

    public string? ResumeUploaded { get; set; }

    public string[]? ServicesOffered { get; set; }

    public decimal HourlyRate { get; set; }
}