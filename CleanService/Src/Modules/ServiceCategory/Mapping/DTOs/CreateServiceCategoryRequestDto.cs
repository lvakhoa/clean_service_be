using System.ComponentModel.DataAnnotations;

namespace CleanService.Src.Modules.ServiceCategory.Mapping.DTOs;

public class CreateServiceCategoryRequestDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
}