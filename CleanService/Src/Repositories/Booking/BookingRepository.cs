using CleanService.Src.Models;
using CleanService.Src.Modules.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Repositories.Booking;

public class BookingRepository : Repository<Bookings>, IBookingRepository
{
    private readonly CleanServiceContext _dbContext;

    public BookingRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}