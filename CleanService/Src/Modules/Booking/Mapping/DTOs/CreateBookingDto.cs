using System.ComponentModel.DataAnnotations;

namespace CleanService.Src.Modules.Booking.Mapping.DTOs;

public class CreateBookingDto
{
    [Required]
    public string CustomerId { get; set; } = null!;

    [Required]
    public string ServiceTypeId { get; set; } = null!;

    public string? Location { get; set; }

    [Required] 
    public DateTime ScheduledStartTime { get; set; }

    [Required] 
    public DateTime ScheduledEndTime { get; set; }

    [MaxLength(50)]
    public string? PaymentMethod { get; set; }
}
