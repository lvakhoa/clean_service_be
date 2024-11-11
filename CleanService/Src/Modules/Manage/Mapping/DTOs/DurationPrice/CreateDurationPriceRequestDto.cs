using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Manage.Mapping.DTOs.DurationPrice;

public class CreateDurationPriceRequestDto
{
    [Required]
    [Precision(3, 2)]
    public decimal PriceMultiplier { get; set; }
    
    [Required]
    public Guid ServiceTypeId { get; set; }
    
    [Required]
    public int DurationHours { get; set; }
}