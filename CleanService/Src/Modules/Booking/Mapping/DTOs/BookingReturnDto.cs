namespace CleanService.Src.Modules.Booking.Mapping.DTOs;


public class BookingReturnDto
{
    public string Id { get; set; } = null!;
    
    public string CustomerId { get; set; } = null!;
    
    public string? HelperId { get; set; }
    
    public string ServiceTypeId { get; set; } = null!;
    
    public string? Location { get; set; }
    
    public DateTime ScheduledStartTime { get; set; }
    
    public DateTime ScheduledEndTime { get; set; }
    
    public string Status { get; set; } = null!;
    
    public string? CancellationReason { get; set; }
    
    public decimal TotalPrice { get; set; }
    
    public string PaymentStatus { get; set; } = null!;
    
    public string? PaymentMethod { get; set; }
    
    public decimal? HelperRating { get; set; }
    
    public string? CustomerFeedback { get; set; }
    
    public string? HelperFeedback { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public UserBookingReturn Customer { get; set; } = null!;
    
    public UserBookingReturn? Helper { get; set; }
    
    public ServiceTypeBookingReturn ServiceType { get; set; } = null!;
    
    public BookingDetailsReturn BookingDetails { get; set; } = null!;
}

public class UserBookingReturn
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

public class ServiceTypeBookingReturn
{
    public string Id { get; set; } = null!;
    
    public string CategoryId { get; set; } = null!;
    
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    
    public decimal BasePrice { get; set; }
    
    public DateTime CreatedAt { get; set; }
}

public class BookingDetailsReturn
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