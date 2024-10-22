using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.DTOs;

namespace CleanService.Src.Modules.Booking.Repositories
{
    public interface IBookingRepository
    {
        public Task<BookingReturnDto?> GetBookingById(Guid id);
        
        public Task<BookingReturnDto> CreateBooking(CreateBookingDto createBooking);
        
        public Task<BookingReturnDto?> UpdateBooking(Guid id, UpdateBookingDto updateBooking);
        
        public Task<BookingReturnDto[]> GetAllBookings();
    }
}