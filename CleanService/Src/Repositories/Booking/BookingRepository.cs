using CleanService.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Repositories.Booking;

public class BookingRepository : Repository<Bookings, PartialBookings>, IBookingRepository
{
    private readonly CleanServiceContext _dbContext;

    public BookingRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}