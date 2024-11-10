using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanService.Src.Models;

public class Feedbacks
{
    [Key] public Guid Id { get; set; }

    [ForeignKey(nameof(Booking))]
    [Required]
    public Guid BookingId { get; set; }

    [Required] public string Title { get; set; } = null!;

    [Required] public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Bookings Booking { get; set; } = null!;
}

public class PartialFeedback
{
    public Guid? BookingId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }
}