using CleanService.Src.Models;

namespace CleanService.Src.Modules.Booking.Mapping.DTOs;

public class CusFeedbackResponseDto
{
    public Guid Id { get; set; }
    
    public Guid BookingId { get; set; }

    public string Title { get; set; } = null!;

   public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public BookingResponse Booking { get; set; } = null!;
}

