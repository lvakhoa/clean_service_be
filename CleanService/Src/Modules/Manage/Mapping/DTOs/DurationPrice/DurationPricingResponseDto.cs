using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Manage.Mapping.DTOs.DurationPrice;

public class DurationPricingResponseDto
{
    [Required]
    public string Id { get; set; } = null!;
    
    [Required]
    public string ServiceTypeId { get; set; } = null!;
    
    [Required]
    public string ServiceTypeName { get; set; } = null!;

    [Required]
    public int DurationHours { get; set; }

    [Required]
    [Precision(3, 2)]
    public decimal PriceMultiplier { get; set; }
}