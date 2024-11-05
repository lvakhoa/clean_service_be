using CleanService.Src.Modules.Booking.Mapping.DTOs;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Scheduler.Services;

public interface ISchedulerService
{
    
    //Task<Pagination<BookingResponseDto>> GetScheduledBookingByHelperId(string helperId, int? page, int? limit);
    Task<Pagination<BookingResponseDto>> QueryScheduledBooking(string? helperId,string? customerId, int? page, int? limit);
    // Task<Pagination<BookingResponseDto>> GetScheduledBookingByCustomerId(string customerId, int? page, int? limit);
    // Task<Pagination<BookingResponseDto>> GetAllScheduledBookings(int? page, int? limit);
    
    Task<BookingResponseDto?> GetScheduledBookingById(Guid id);
    
    
    Task CancelScheduledBooking(Guid bookingId);
}