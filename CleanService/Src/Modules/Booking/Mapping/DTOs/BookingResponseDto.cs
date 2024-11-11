namespace CleanService.Src.Modules.Booking.Mapping.DTOs;


public class BookingResponseDto
{
    public string Id { get; set; } = null!;
    
    public string CustomerId { get; set; } = null!;
    
    public string? HelperId { get; set; }
    
    public string ServiceTypeId { get; set; } = null!;
    
    public string Location { get; set; } = null!;
    
    public DateTime ScheduledStartTime { get; set; }
    
    public DateTime ScheduledEndTime { get; set; }
    
    public string Status { get; set; } = null!;
    
    public string? CancellationReason { get; set; }
    
    public decimal TotalPrice { get; set; }
    
    public string PaymentStatus { get; set; } = null!;
    
    public string? PaymentMethod { get; set; }
    
    public decimal? HelperRating { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public UserBookingResponse Customer { get; set; } = null!;
    
    public HelperBookingResponse? Helper { get; set; }
    
    public ServiceTypeBookingResponse ServiceType { get; set; } = null!;
    
    public BookingDetailsResponse BookingDetails { get; set; } = null!;
    
    public List<FeedbackResponse>? Feedbacks { get; set; }

    public List<RefundResponse>? Refunds { get; set; }
}

public class UserBookingResponse
{
    public string Id { get; set; } = null!;
    
    public string? Gender { get; set; }
    
    public string FullName { get; set; } = null!;
    
    public string? IdentityCard { get; set; }
    
    public string? Address { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public string Email { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}

public class HelperBookingResponse
{
    public string Id { get; set; } = null!;

    public string? ExperienceDescription { get; set; } = null!;

    public string[]? ServicesOffered { get; set; }

    public decimal HourlyRate { get; set; }

    public decimal AverageRating { get; set; }
    
    public UserBookingResponse User { get; set; } = null!;
}

public class ServiceTypeBookingResponse
{
    public string Id { get; set; } = null!;
    
    public string CategoryId { get; set; } = null!;
    
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    
    public decimal BasePrice { get; set; }
    
    public DateTime CreatedAt { get; set; }
}

public class BookingDetailsResponse
{
    public string Id { get; set; } = null!;
    
    public string BookingId { get; set; } = null!;
    
    public string? DurationPriceId { get; set; }
    
    public int BedroomCount { get; set; }
    
    public int BathroomCount { get; set; }
    
    public int KitchenCount { get; set; }
    
    public int LivingRoomCount { get; set; }
    
    public string? SpecialRequirements { get; set; }
    
    public DateTime CreatedAt { get; set; }
}

public class FeedbackResponse
{
    public string Id { get; set; } = null!;
    
    public string BookingId { get; set; } = null!;
    
    public string Title { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}

public class RefundResponse
{
    public string Id { get; set; } = null!;
    
    public string BookingId { get; set; } = null!;
    
    public string Reason { get; set; } = null!;
    
    public string  Status { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? ResolvedAt { get; set; }
}