namespace CleanService.Src.Modules.Booking.Mapping.DTOs;

public class CreateComplaintDto
{
    public Guid BookingId { get; set; }
    
    public string ReportedById { get; set; } = null!;
    
    public string ReportedUserId { get; set; } = null!;
    
    public string Reason { get; set; } = null!;
    
    public string? Resolution { get; set; }
    
    public DateTime ResolvedAt { get; set; }
}