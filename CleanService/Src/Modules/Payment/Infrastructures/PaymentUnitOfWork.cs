using CleanService.Src.Models;
using CleanService.Src.Repositories.Booking;
using CleanService.Src.Repositories.Notification;

namespace CleanService.Src.Modules.Payment.Infrastructures;

public class PaymentUnitOfWork : IPaymentUnitOfWork
{
    private readonly CleanServiceContext _dbContext;

    public INotificationRepository NotificationRepository { get; }

    public IBookingRepository BookingRepository { get; }

    public PaymentUnitOfWork(CleanServiceContext dbContext,
        INotificationRepository notificationRepository, IBookingRepository bookingRepository)
    {
        _dbContext = dbContext;
        NotificationRepository = notificationRepository;
        BookingRepository = bookingRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}