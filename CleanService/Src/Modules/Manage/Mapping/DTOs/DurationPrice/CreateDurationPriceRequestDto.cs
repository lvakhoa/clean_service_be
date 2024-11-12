using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Manage.Mapping.DTOs.DurationPrice;

public class CreateDurationPriceRequestDto
{
    [Required]
    [Precision(3, 2)]
    [Range(0, double.MaxValue, ErrorMessage = "Price Multiplier must not be less than 0.")]
    public decimal PriceMultiplier { get; set; }
    
    [Required]
    public Guid ServiceTypeId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Duration hours must be at least 1.")]
    public int DurationHours { get; set; }
}