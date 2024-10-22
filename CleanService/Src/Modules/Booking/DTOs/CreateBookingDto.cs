using System.ComponentModel.DataAnnotations;
using CleanService.Src.Models;

namespace CleanService.Src.Modules.Booking.DTOs;

public class CreateBookingDto
{
    [Required]
    public string Id { get; set; }
    
    [Required]
    public string UserId { get; set; }
    
    [Required]
    public string HelperId { get; set; }
    
    [Required]
    public DateTime BookingDate { get; set; }
    
    [Required]
    public string BookingTime { get; set; }
    
    [Required]
    public string BookingAddress { get; set; }
    
    [Required]
    public string BookingDescription { get; set; }
    
    [Required]
    public BookingStatus BookingStatus { get; set; }
}