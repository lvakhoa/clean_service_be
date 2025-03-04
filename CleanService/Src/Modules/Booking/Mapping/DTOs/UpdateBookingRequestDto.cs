using CleanService.Src.Models;
using CleanService.Src.Models.Configurations;

using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Booking.Mapping.DTOs
{
    public class UpdateBookingRequestDto
    {
        //public string Id { get; set; } = null!;

        public string? HelperId { get; set; }

        public string? Location { get; set; }

        public DateTime? ScheduledStartTime { get; set; }

        public DateTime? ScheduledEndTime { get; set; }

        public BookingStatus? Status { get; set; }

        public string? CancellationReason { get; set; }

        [Precision(10, 2)] public decimal? TotalPrice { get; set; }

        public PaymentStatus? PaymentStatus { get; set; }

        [Precision(2, 1)] public decimal? HelperRating { get; set; }

        public string? CustomerFeedback { get; set; }

        public string? HelperFeedback { get; set; }
    }
}
