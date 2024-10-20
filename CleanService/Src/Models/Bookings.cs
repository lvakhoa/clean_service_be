using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Models;

public class Bookings
{
    [Key] [MaxLength(255)] public Guid Id { get; set; }

    [ForeignKey(nameof(Users))]
    [Required]
    [MaxLength(255)]
    public string CustomerId { get; set; } = null!;

    [ForeignKey(nameof(Users))]
    [MaxLength(255)]
    [Comment("Can be NULL if not assigned yet")]
    public string? HelperId { get; set; }

    [ForeignKey(nameof(Services))]
    [Required]
    [MaxLength(255)]
    public Guid ServiceId { get; set; }

    [MaxLength(255)] public string? Location { get; set; }

    [Required] public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public BookingStatus Status { get; set; }

    public string? CancellationReason { get; set; }

    [Precision(10, 2)]
    [Range(1000, double.MaxValue)]
    public decimal? Price { get; set; }

    [MaxLength(50)] public string? PaymentMethod { get; set; }

    public int? Rating { get; set; }

    public string? Feedback { get; set; }

    public virtual Users Customer { get; set; } = null!;
    
    public virtual Users? Helper { get; set; }

    public virtual Services Service { get; set; } = null!;
    
    public virtual Contracts Contract { get; set; } = null!;
}

public enum BookingStatus
{
    Pending,
    Confirmed,
    InProgress,
    Completed,
    Cancelled
}