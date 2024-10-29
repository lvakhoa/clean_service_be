using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Auth.DTOs;

public class UpdateHelperDto
{
    public string? ExperienceDescription { get; set; }

    public string[]? ServicesOffered { get; set; }
    
    [Precision(10, 2)]
    [Range(1000, double.MaxValue)]
    public decimal? ProposedPrice { get; set; }
}