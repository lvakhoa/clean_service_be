using CleanService.Src.Modules.Booking.Mapping.DTOs;
using Pagination.EntityFrameworkCore.Extensions;

namespace  CleanService.Src.Modules.Booking.Services;

public interface IBookingService
{
    Task CreateBooking(CreateBookingRequestDto createBookingDto);
    
    Task UpdateBooking(Guid id, UpdateBookingRequestDto updateBookingDto);
    
    Task<Pagination<BookingResponseDto>> GetAllBookings(int? page, int? limit);

    Task<BookingResponseDto?> GetBookingById(Guid id);
}

