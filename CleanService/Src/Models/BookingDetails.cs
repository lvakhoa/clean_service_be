using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanService.Src.Models;

public class BookingDetails
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [ForeignKey(nameof(Booking))]
    public Guid BookingId { get; set; }
    
    public int BedroomCount { get; set; }
    
    public int BathroomCount { get; set; }
    
    public int KitchenCount { get; set; }
    
    public int LivingRoomCount { get; set; }
    
    public string? SpecialRequirements { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    public virtual Bookings Booking { get; set; } = null!;
}

public class PartialBookingDetails
{
    public Guid? Id { get; set; }
    
    public Guid? BookingId { get; set; }
    
    public int?BedroomCount { get; set; }
    
    public int?BathroomCount { get; set; }
    
    public int?KitchenCount { get; set; }
    
    public int? LivingRoomCount { get; set; }
    
    public string? SpecialRequirements { get; set; }
    
    public DateTime? CreatedAt { get; set; }
}