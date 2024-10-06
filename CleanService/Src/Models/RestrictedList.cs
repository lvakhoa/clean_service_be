using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanService.Src.Models;

public class RestrictedList
{
    [Key]
    [MaxLength(255)] public Guid Id { get; set; }
    
    [ForeignKey(nameof(Users))]
    [Required]
    [MaxLength(255)]
    public Guid CustomerId { get; set; }
    
    [ForeignKey(nameof(Users))]
    [Required]
    [MaxLength(255)]
    public Guid HelperId { get; set; }
    
    public virtual Users Customer { get; set; } = null!;
    
    public virtual Users Helper { get; set; } = null!;
}
