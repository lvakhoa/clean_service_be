using System.ComponentModel.DataAnnotations;

namespace CleanService.Src.Modules.ServiceType.Mapping.DTOs;

public class UpdateServiceTypeRequestDto
{
    public string Id { get; set; } = null!;
    
    public string? CategoryId { get; set; }
    
    [MaxLength(100)]
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public decimal? BasePrice { get; set; }
}