using CleanService.Src.Models;

namespace CleanService.Src.Repositories.Booking
{
    public interface IBookingRepository : IRepository<Bookings, PartialBookings>
    {
        public Task<Bookings[]> GetBookingByUserId(IEnumerable<BookingStatus>? statuses, string id, UserType userType);
    }
}