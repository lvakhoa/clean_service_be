using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CleanService.Src.Common;

using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Models;

[Index(nameof(Name), nameof(CategoryId), IsUnique = true)]
public class ServiceTypes : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey(nameof(Category))]
    [Required]
    public Guid CategoryId { get; set; }

    [Required]
    [MaxLength(100)]
    [Comment("e.g., 'Standard Cleaning', 'Deep Clean'")]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [Required]
    public decimal BasePrice { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public bool IsActive { get; set; }

    public virtual ServiceCategories Category { get; set; } = null!;

    public virtual ICollection<RoomPricing> RoomPricing { get; set; } = new List<RoomPricing>();

    public virtual ICollection<DurationPrice> DurationPrice { get; set; } = new List<DurationPrice>();

    public virtual ICollection<Bookings> Bookings { get; set; } = new List<Bookings>();
}

public class PartialServiceTypes : BaseEntity
{
    //public Guid? Id { get; set; }

    public Guid? CategoryId { get; set; }

    [MaxLength(100)]
    public string? Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? BasePrice { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }
}
