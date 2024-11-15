namespace CleanService.Src.Modules.Booking.Mapping.DTOs;

public class UpdateFeedbackDto
{
    public Guid? BookingId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }
}