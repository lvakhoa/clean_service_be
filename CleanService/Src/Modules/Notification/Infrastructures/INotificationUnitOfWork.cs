using CleanService.Src.Repositories.Notification;
using CleanService.Src.Repositories.User;

namespace CleanService.Src.Modules.Notification.Infrastructures;

public interface INotificationUnitOfWork
{
    INotificationRepository NotificationRepository { get; }

    IUserRepository UserRepository { get; }

    Task SaveChangesAsync();
}