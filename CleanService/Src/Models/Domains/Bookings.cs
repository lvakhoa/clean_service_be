using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CleanService.Src.Common;
using CleanService.Src.Models.Configurations;

using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Models;

[Index(nameof(OrderId), IsUnique = true)]
public class Bookings : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public int OrderId { get; set; }

    [ForeignKey(nameof(Customer))]
    [Required]
    public string CustomerId { get; set; } = null!;

    [ForeignKey(nameof(Helper))]
    [Comment("Can be NULL if not assigned yet")]
    public string? HelperId { get; set; }

    [ForeignKey(nameof(ServiceType))]
    [Required]
    public Guid ServiceTypeId { get; set; }

    [Required]
    public string Location { get; set; } = null!;

    [Required]
    public DateTime ScheduledStartTime { get; set; }

    [Required]
    public DateTime ScheduledEndTime { get; set; }

    [Column(TypeName = "varchar(24)")]
    [Required]
    public BookingStatus Status { get; set; }

    public string? CancellationReason { get; set; }

    [Precision(10, 2)]
    [Required]
    public decimal TotalPrice { get; set; }

    [Required]
    [Column(TypeName = "varchar(24)")]
    public PaymentStatus PaymentStatus { get; set; }

    [MaxLength(50)]
    public string? PaymentMethod { get; set; }

    [Precision(2, 1)]
    public decimal? HelperRating { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Users Customer { get; set; } = null!;

    public virtual Helpers? Helper { get; set; }

    public virtual ServiceTypes ServiceType { get; set; } = null!;

    public virtual BookingContracts Contract { get; set; } = null!;

    public virtual BookingDetails BookingDetails { get; set; } = null!;

    public virtual ICollection<Feedbacks> Feedbacks { get; set; } = new List<Feedbacks>();

    public virtual ICollection<Refunds> Refunds { get; set; } = new List<Refunds>();
}

public class PartialBookings : BaseEntity
{
    //public Guid? Id { get; set; }

    public string? CustomerId { get; set; } = null!;

    public string? HelperId { get; set; }

    public Guid? ServiceTypeId { get; set; }

    public string? Location { get; set; }

    public DateTime? ScheduledStartTime { get; set; }

    public DateTime? ScheduledEndTime { get; set; }

    [Column(TypeName = "varchar(24)")]
    public BookingStatus? Status { get; set; }

    public string? CancellationReason { get; set; }

    [Precision(10, 2)]
    public decimal? TotalPrice { get; set; }

    [Column(TypeName = "varchar(24)")]
    public PaymentStatus? PaymentStatus { get; set; }

    [MaxLength(50)]
    public string? PaymentMethod { get; set; }

    [Precision(2, 1)]
    public decimal? HelperRating { get; set; }

    public string? CustomerFeedback { get; set; }

    public string? HelperFeedback { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
