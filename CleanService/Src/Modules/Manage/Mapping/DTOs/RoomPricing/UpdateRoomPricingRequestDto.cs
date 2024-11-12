using System.ComponentModel.DataAnnotations;
using CleanService.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Manage.Mapping.DTOs.RoomPricing;

public class UpdateRoomPricingRequestDto
{
    public Guid? ServiceTypeId { get; set; }

    [EnumDataType(typeof(RoomType), ErrorMessage = "Invalid room type specified.")]
    public RoomType? RoomType { get; set; }
    
    [Range(0, 5, ErrorMessage = "Room count must be between 0 and 5.")]
    public int? RoomCount { get; set; }
    
    [Range(0, double.MaxValue, ErrorMessage = "Additional price must not be less than 0.")]
    [Precision(10, 2)]
    public decimal? AdditionalPrice { get; set; }
}