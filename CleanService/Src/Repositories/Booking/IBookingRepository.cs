using CleanService.Src.Models;

namespace CleanService.Src.Repositories.Booking
{
    public interface IBookingRepository : IRepository<Bookings, PartialBookings>
    {
    }
}