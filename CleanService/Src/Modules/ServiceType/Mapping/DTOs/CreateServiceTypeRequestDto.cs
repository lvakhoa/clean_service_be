using System.ComponentModel.DataAnnotations;

namespace CleanService.Src.Modules.ServiceType.Mapping.DTOs;

public class CreateServiceTypeRequestDto
{
    [Required]
    public string CategoryId { get; set; } = null!;
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    
    [Required]
    public decimal BasePrice { get; set; }
}