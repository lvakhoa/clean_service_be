using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CleanService.Src.Common;

namespace CleanService.Src.Models.Domains;

public class BookingContracts : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey(nameof(Booking))]
    [Required]
    public Guid BookingId { get; set; }

    [Required]
    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Bookings Booking { get; set; } = null!;
}

public class PartialBookingContracts : BaseEntity
{
    public Guid? Id { get; set; }

    public Guid? BookingId { get; set; }

    public string? Content { get; set; }
}
