using System.ComponentModel.DataAnnotations;

namespace CleanService.Src.Modules.Booking.Mapping.DTOs;

public class CreateBookingRequestDto
{
    [Required]
    public string CustomerId { get; set; } = null!;

    [Required]
    public string ServiceTypeId { get; set; } = null!;

    public string Location { get; set; } = null!;

    [Required] 
    public DateTime ScheduledStartTime { get; set; }

    [Required] 
    public DateTime ScheduledEndTime { get; set; }

    [MaxLength(50)]
    public string? PaymentMethod { get; set; }
    
    public CreateBookingDetails BookingDetails { get; set; } = null!;
    
    public string ContractContent { get; set; } = null!;
}

public class CreateBookingDetails
{
    public string DurationPriceId { get; set; } = null!;
    
    public int BedroomCount { get; set; }
    
    public int BathroomCount { get; set; }
    
    public int KitchenCount { get; set; }
    
    public int LivingRoomCount { get; set; }
    
    public string? SpecialRequirements { get; set; }
}
