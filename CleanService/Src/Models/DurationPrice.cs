using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Models;

[Index(nameof(ServiceTypeId), nameof(DurationHours), IsUnique = true)]
public class DurationPrice
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(ServiceType))]
    [Required]
    public Guid ServiceTypeId { get; set; }
    
    [Required]
    public int DurationHours { get; set; }
    
    [Required]
    [Precision(3, 2)]
    public decimal PriceMultiplier { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public virtual ServiceTypes ServiceType { get; set; } = null!;
}