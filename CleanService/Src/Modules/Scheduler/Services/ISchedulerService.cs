using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace CleanService.Src.Modules.Scheduler.Services;

public interface ISchedulerService
{
    Task<BookingReturnDto[]> GetAllScheduledBookings();
    
    Task<BookingReturnDto?> GetScheduledBookingById(Guid id);
    
    Task<BookingReturnDto?> CancelScheduledBooking(Guid bookingId);
}