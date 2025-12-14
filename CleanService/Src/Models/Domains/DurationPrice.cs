using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CleanService.Src.Common;

using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Models.Domains;

[Index(nameof(ServiceTypeId), nameof(DurationHours), IsUnique = true)]
public class DurationPrice : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey(nameof(ServiceType))]
    [Required]
    public Guid ServiceTypeId { get; set; }

    [Required]
    public int DurationHours { get; set; }

    [Required]
    [Precision(3, 2)]
    public decimal PriceMultiplier { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ServiceTypes ServiceType { get; set; } = null!;

    public virtual ICollection<BookingDetails> BookingDetails { get; set; } = new List<BookingDetails>();
}

public class PartialDurationPrice : BaseEntity
{
    //public Guid? Id { get; set; }

    public Guid? ServiceTypeId { get; set; }

    public int? DurationHours { get; set; }

    [Precision(3, 2)]
    public decimal? PriceMultiplier { get; set; }

    public DateTime? CreatedAt { get; set; }
}
