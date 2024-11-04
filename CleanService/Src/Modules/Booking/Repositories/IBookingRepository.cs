using CleanService.Src.Models;
using CleanService.Src.Modules.Booking.Mapping.DTOs;

namespace CleanService.Src.Modules.Booking.Repositories
{
    public interface IBookingRepository
    {
        public Task<Bookings?> GetBookingById(Guid id);
        
        public Task<Bookings> CreateBooking(Bookings createBooking);
        
        public Task<Bookings?> UpdateBooking(Guid id, PartialBookings updateBooking);
        
        public Task<Bookings[]> GetAllBookings();

        public Task<Bookings[]> GetBookingByUserId(IEnumerable<BookingStatus>? statuses, string id, UserType userType);


    }
}