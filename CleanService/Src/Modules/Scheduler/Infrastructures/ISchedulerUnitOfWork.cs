using CleanService.Src.Repositories.Booking;

namespace CleanService.Src.Modules.Scheduler.Infrastructures;

public interface ISchedulerUnitOfWork
{
    IBookingRepository BookingRepository { get; }
    
    Task SaveChangesAsync();
}