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
}

public class ServiceCategoryReturn
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; }
}