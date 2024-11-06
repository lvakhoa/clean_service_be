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
    
    public async Task<Bookings[]> GetBookingByUserId(IEnumerable<BookingStatus>? statuses, string id, UserType userType)
    {
        IQueryable<Bookings> query;

        if (userType == UserType.Helper)
        {
            query = _dbContext.Bookings.Where(x => x.Helper.Id == id);
        } else
        {
            query = _dbContext.Bookings.Where(x => x.Customer.Id == id);
        }

        if (statuses != null && statuses.Any())
        {
            query = query.Where(x => statuses.Contains(x.Status));
        }

        return await query.ToArrayAsync();
    }
}