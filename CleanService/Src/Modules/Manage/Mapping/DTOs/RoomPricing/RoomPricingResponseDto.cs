namespace CleanService.Src.Modules.Manage.Mapping.DTOs;

public class RoomPricingResponseDto
{
    public Guid Id { get; init; }
    public Guid ServiceTypeId { get; init; }
    public string ServiceTypeName { get; init; } = null!;
    public int RoomCount { get; init; }
    public string RoomType { get; init; } = null!;
    public decimal AdditionalPrice { get; init; }
    public DateTime CreatedAt { get; init; }
}