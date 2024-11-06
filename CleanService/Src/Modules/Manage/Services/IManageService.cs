using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;
using Pagination.EntityFrameworkCore.Extensions;

namespace  CleanService.Src.Modules.Manage.Services;

public interface IManageService
{
    Task CreateBooking(CreateBookingRequestDto createBookingDto);
    
    Task UpdateBooking(Guid id, UpdateBookingRequestDto updateBookingDto);
    
    Task<Pagination<BookingResponseDto>> GetAllBookings(int? page, int? limit);

    Task<BookingResponseDto?> GetBookingById(Guid id);
    
    Task<string?> AssignHelperToBooking(Bookings booking);
}

