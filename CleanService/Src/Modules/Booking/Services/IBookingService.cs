using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.DTOs;

namespace  CleanService.Src.Modules.Booking.Services;

public interface IBookingService
{
    Task<BookingReturnDto> CreateBooking(CreateBookingDto createBookingDto);
    
    Task<BookingReturnDto[]> GetAllBookings();
}

