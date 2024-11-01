using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace  CleanService.Src.Modules.Booking.Services;

public interface IBookingService
{
    Task<BookingReturnDto> CreateBooking(CreateBookingDto createBookingDto);
    
    Task<BookingReturnDto?> UpdateBooking(Guid id, UpdateBookingDto updateBookingDto);
    
    Task<BookingReturnDto[]> GetAllBookings();

    Task<BookingReturnDto?> GetBookingById(Guid id);
}

