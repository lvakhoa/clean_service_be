namespace CleanService.Src.Modules.Service.DTOs;

public class ServiceTypeReturnDto
{
    public string Id { get; set; } = null!;

    public string CategoryId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal BasePrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; }

    public ServiceCategoryReturn Category { get; set; } = null!;
    
    public List<RoomPricingReturn> RoomPricing { get; set; } = new List<RoomPricingReturn>();
    
    public List<DurationPriceReturn> DurationPrice { get; set; } = new List<DurationPriceReturn>();
}

public class ServiceCategoryReturn
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; }
}

public class RoomPricingReturn
{
    public string Id { get; set; } = null!;

    public string ServiceTypeId { get; set; } = null!;

    public string RoomType { get; set; } = null!;
    
    public int RoomCount { get; set; }

    public decimal AdditionalPrice { get; set; }

    public DateTime CreatedAt { get; set; }
}

public class DurationPriceReturn
{
    public string Id { get; set; } = null!;

    public string ServiceTypeId { get; set; } = null!;

    public int DurationHours { get; set; }

    public decimal PriceMultiplier { get; set; }

    public DateTime CreatedAt { get; set; }
}