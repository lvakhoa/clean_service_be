namespace CleanService.Src.Modules.Booking.Mapping.DTOs;

public class CreateRefundRequestDto
{
    public Guid BookingId { get; set; }
    
    public string Reason { get; set; } = null!;
    
    //public DateTime ResolvedAt { get; set; }
}