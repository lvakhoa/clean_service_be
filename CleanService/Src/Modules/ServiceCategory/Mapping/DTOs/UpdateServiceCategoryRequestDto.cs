using System.ComponentModel.DataAnnotations;

namespace CleanService.Src.Modules.ServiceCategory.Mapping.DTOs;

public class UpdateServiceCategoryRequestDto
{
    [MaxLength(50)]
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public bool? IsActive { get; set; }
}