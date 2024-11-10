using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanService.Src.Models;

public class Refunds
{
    [Key] public Guid Id { get; set; }

    [ForeignKey(nameof(Booking))]
    [Required]
    public Guid BookingId { get; set; }

    [Required] public string Reason { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(24)")]
    public RefundStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ResolvedAt { get; set; }

    public virtual Bookings Booking { get; set; } = null!;
}

public enum RefundStatus
{
    Pending,
    Refunded,
    Declined
}

public class PartialRefunds
{
    public Guid? BookingId { get; set; }

    public string? Reason { get; set; }

    public RefundStatus? Status { get; set; }
}