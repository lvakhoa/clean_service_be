using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanService.Src.Models;

public class BookingContracts
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

public class PartialBookingContracts
{
    public Guid? Id { get; set; }
    
    public Guid? BookingId { get; set; }

    public string? Content { get; set; }
}