using System.ComponentModel.DataAnnotations;
using CleanService.Src.Models;

namespace CleanService.Src.Modules.Booking.DTOs;

public class CreateBookingDto
{
    [Required]
    public string CustomerId { get; set; } = null!;

    public string? HelperId { get; set; }

    [Required]
    public Guid ServiceId { get; set; }

    public string? Location { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    // [Range(1000, double.MaxValue)]
    // public decimal? Price { get; set; }

    public string? PaymentMethod { get; set; }
}
