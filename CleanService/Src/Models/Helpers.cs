using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Models;

[Comment("Additional information for users' type is helper")]
public class Helpers
{
    [Key]
    [ForeignKey(nameof(Users))]
    [MaxLength(255)]
    public string Id { get; set; } = null!;
    
    public string? ExperienceDescription { get; set; }
    
    [MaxLength(255)]
    public string? ServicesOffered { get; set; }
    
    [Precision(10, 2)]
    [Range(1000, double.MaxValue)]
    public decimal? ProposedPrice { get; set; }

    [Precision(3, 2)]
    [Range(0, 100)]
    public decimal AverageRating { get; set; } 
    
    public int CompletedJobs { get; set; } 
    
    public int CancelledJobs { get; set; } 
    
    public virtual Users User { get; set; } = null!;
}