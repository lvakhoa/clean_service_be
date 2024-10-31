using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Auth.Mapping.DTOs;

public class UpdateHelperDto
{
    public string? ExperienceDescription { get; set; }
    
    public string? ResumeUploaded { get; set; }

    public string[]? ServicesOffered { get; set; }
}