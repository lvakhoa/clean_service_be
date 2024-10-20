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
    public string CustomerId { get; set; } = null!;
    
    [ForeignKey(nameof(Users))]
    [Required]
    [MaxLength(255)]
    public string HelperId { get; set; } = null!;
    
    public virtual Users Customer { get; set; } = null!;
    
    public virtual Users Helper { get; set; } = null!;
}
