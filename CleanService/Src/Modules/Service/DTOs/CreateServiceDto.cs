using System.ComponentModel.DataAnnotations;

namespace CleanService.Src.Modules.Service.DTOs;

public class CreateServiceDto
{
    [Required]
    public string ServiceName { get; set; } = null!;
    
    public string? Description { get; set; }
}