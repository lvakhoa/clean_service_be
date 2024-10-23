using CleanService.Src.Models;

namespace CleanService.Src.Modules.Booking.DTOs;


public class BookingReturnDto
{
    public Guid Id { get; set; }
    
    public string CustomerId { get; set; } = null!;
    
    public string? HelperId { get; set; }
    
    public Guid ServiceId { get; set; }
    
    public string? Location { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime? EndTime { get; set; }
    
    public string Status { get; set; }
    
    public string? CancellationReason { get; set; }
    
    public decimal? Price { get; set; }
    
    public string? PaymentMethod { get; set; }
    
    public int? Rating { get; set; }
    
    public string? Feedback { get; set; }
}
