using CleanService.Src.Attributes;
using CleanService.Src.Models;

namespace CleanService.Src.Modules.ServiceCategory.Mapping.DTOs;

public class ServiceCategoryResponseDto
{
    public string Id { get; set; } = null!;
    
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
    
    [RoleExpose(UserType.Admin)]
    public bool IsActive { get; set; }
    
    [RoleExpose(UserType.Admin)]
    public DateTime CreatedAt { get; set; }
}