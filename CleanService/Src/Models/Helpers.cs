using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Models;

[Comment("Additional information for users' type is helper")]
public class Helpers
{
    [Key]
    [ForeignKey(nameof(User))]
    public string Id { get; set; } = null!;
    
    public string? ExperienceDescription { get; set; }
    
    public string? ResumeUploaded { get; set; }
    
    [Comment("Array of service_type_ids")]
    public Guid[]? ServicesOffered { get; set; }
    
    [Required]
    [Precision(10, 2)]
    public decimal HourlyRate { get; set; }
    
    [Precision(2, 1)]
    public decimal AverageRating { get; set; } 
    
    public int CompletedJobs { get; set; } 
    
    public int CancelledJobs { get; set; } 
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    [Required]
    public DateTime UpdatedAt { get; set; }
    
    public virtual Users User { get; set; } = null!;
}

public class PartialHelper
{
    public string? Id { get; set; }
    
    public string? ExperienceDescription { get; set; }
    
    public string? ResumeUploaded { get; set; }
    
    public Guid[]? ServicesOffered { get; set; }
    
    [Precision(10, 2)]
    public decimal? HourlyRate { get; set; }
    
    [Precision(2, 1)]
    public decimal? AverageRating { get; set; } 
    
    public int? CompletedJobs { get; set; } 
    
    public int? CancelledJobs { get; set; } 
    
    public DateTime? CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}