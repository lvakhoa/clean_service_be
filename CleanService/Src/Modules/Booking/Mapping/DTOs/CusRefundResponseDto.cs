namespace CleanService.Src.Modules.Booking.Mapping.DTOs;

public class CusRefundResponseDto
{
    public string Id { get; set; } = null!;
    
    public string BookingId { get; set; } = null!;
    
    public string Reason { get; set; } = null!;
    
    public string Status { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? ResolvedAt { get; set; }
    
    public BookingResponse Booking { get; set; } = null!;
}

public class BookingResponse
{
    public string Id { get; set; } = null!;
    
    public string CustomerId { get; set; } = null!;
    
    public string? HelperId { get; set; }
    
    public string ServiceTypeId { get; set; } = null!;
    
    public string Location { get; set; }
    
    public DateTime ScheduledStartTime { get; set; }
    
    public DateTime ScheduledEndTime { get; set; }
    
    public string Status { get; set; } = null!;
    
    public string? CancellationReason { get; set; }
    
    public decimal TotalPrice { get; set; }
    
    public string PaymentStatus { get; set; } = null!;
    
    public string? PaymentMethod { get; set; }
    
    public decimal? HelperRating { get; set; }
    
    public string? HelperFeedback { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}
