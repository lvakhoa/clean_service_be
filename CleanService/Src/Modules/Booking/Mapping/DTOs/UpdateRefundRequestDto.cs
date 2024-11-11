using CleanService.Src.Models;

namespace CleanService.Src.Modules.Booking.Mapping.DTOs;

public class UpdateRefundRequestDto
{
    public Guid? BookingId { get; set; }
    
    public string? Reason { get; set; } = null!;
    
    public RefundStatus? Status { get; set; }
    
    public DateTime? ResolvedAt { get; set; }
}