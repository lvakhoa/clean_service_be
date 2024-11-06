using CleanService.Src.Models;
using CleanService.Src.Repositories.Booking;

namespace CleanService.Src.Modules.Scheduler.Infrastructures;

public class SchedulerUnitOfWork : ISchedulerUnitOfWork
{
    private readonly CleanServiceContext _dbContext;
    public IBookingRepository BookingRepository { get; }

    public SchedulerUnitOfWork(CleanServiceContext dbContext ,IBookingRepository bookingRepository)
    {
        _dbContext = dbContext;
        BookingRepository = bookingRepository;
    }
    
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}