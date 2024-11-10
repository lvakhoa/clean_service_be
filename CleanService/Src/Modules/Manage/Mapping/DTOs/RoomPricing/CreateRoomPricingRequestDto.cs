using System.ComponentModel.DataAnnotations;
using CleanService.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Manage.Mapping.DTOs.RoomPricing;
public class CreateRoomPricingRequestDto
{
    [Required]
    public string ServiceTypeId { get; set; } = null!;
    
    [Required]
    public RoomType RoomType { get; set; }
    
    [Required]
    [Range(0, 5)]
    public int RoomCount { get; set; }
    
    [Required]
    [Precision(10, 2)]
    public decimal AdditionalPrice { get; set; }
}