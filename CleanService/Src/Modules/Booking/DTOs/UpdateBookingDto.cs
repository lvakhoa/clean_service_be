using System.ComponentModel.DataAnnotations;
using CleanService.Src.Models;

namespace CleanService.Src.Modules.Booking.DTOs
{
    public class UpdateBookingDto
    {
        public string? BookingAddress { get; set; }
        
        public string? BookingDescription { get; set; }
        
        public BookingStatus? BookingStatus { get; set; }
    }
}