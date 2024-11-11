using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Manage.Mapping.DTOs.DurationPrice;

public class UpdateDurationPriceRequestDto
{
    public Guid? ServiceTypeId { get; set; }
    public int? DurationHours { get; set; }
    
    [Precision(3, 2)]
    public decimal? PriceMultiplier { get; set; }
}