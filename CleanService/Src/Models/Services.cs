using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Models;

[Index(nameof(ServiceName))]
public class Services
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string ServiceName { get; set; } = null!;
    
    public string? Description { get; set; }
}