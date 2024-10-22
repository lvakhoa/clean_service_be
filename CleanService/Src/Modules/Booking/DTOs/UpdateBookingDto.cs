using System.ComponentModel.DataAnnotations;
using CleanService.Src.Models;

namespace CleanService.Src.Modules.Booking.DTOs
{
    public class UpdateBookingDto
    {
        public string? HelperId { get; set; }

        public string? Location { get; set; }

        public DateTime? EndTime { get; set; }

        public BookingStatus? Status { get; set; }

        public string? CancellationReason { get; set; }

        [Range(1000, double.MaxValue)]
        public decimal? Price { get; set; }

        public string? PaymentMethod { get; set; }

        public int? Rating { get; set; }

        public string? Feedback { get; set; }
    }

}