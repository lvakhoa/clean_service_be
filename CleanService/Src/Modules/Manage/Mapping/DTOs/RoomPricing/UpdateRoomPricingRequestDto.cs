using System.ComponentModel.DataAnnotations;
using CleanService.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Manage.Mapping.DTOs.RoomPricing;

public class UpdateRoomPricingRequestDto
{
    public Guid? ServiceTypeId { get; set; }

    public RoomType? RoomType { get; set; }

    [Range(0, 5)]
    public int? RoomCount { get; set; }

    [Precision(10, 2)]
    public decimal? AdditionalPrice { get; set; }
}