using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Models;

[Index(nameof(ServiceTypeId), nameof(RoomType), nameof(RoomCount), IsUnique = true)]
public class RoomPricing
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey(nameof(ServiceType))]
    public Guid ServiceTypeId { get; set; }

    [Required]
    [Column(TypeName = "varchar(24)")]
    public RoomType RoomType { get; set; }

    [Range(0, 5)]
    [Comment("0 represents studio")]
    public int RoomCount { get; set; }

    [Required]
    [Precision(10, 2)]
    public decimal AdditionalPrice { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public virtual ServiceTypes ServiceType { get; set; } = null!;
}

public enum RoomType
{
    Bedroom,
    Bathroom,
    Kitchen,
    LivingRoom,
    Other
}