using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Models;

[Index(nameof(Name), IsUnique = true)]
public class ServiceCategories
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    [Required]
    public bool IsActive { get; set; }
    
    public virtual ICollection<ServiceTypes> ServiceTypes { get; set; } = new List<ServiceTypes>();
}