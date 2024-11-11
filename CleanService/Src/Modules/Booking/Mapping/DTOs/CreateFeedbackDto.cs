namespace CleanService.Src.Modules.Booking.Mapping.DTOs;

public class CreateFeedbackDto
{
    public Guid BookingId { get; set; }
    
    public string Title { get; set; } = null!;
    
    public string Description { get; set; } = null!;
}