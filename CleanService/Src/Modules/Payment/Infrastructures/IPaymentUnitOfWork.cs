using CleanService.Src.Repositories.Booking;
using CleanService.Src.Repositories.Notification;

namespace CleanService.Src.Modules.Payment.Infrastructures;

public interface IPaymentUnitOfWork
{
    INotificationRepository NotificationRepository { get; }

    IBookingRepository BookingRepository { get; }

    Task SaveChangesAsync();
}