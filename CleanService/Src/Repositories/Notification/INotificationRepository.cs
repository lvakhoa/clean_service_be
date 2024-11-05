using CleanService.Src.Models;
using CleanService.Src.Modules.Notification.DTOs;

namespace CleanService.Src.Repositories.Notification;

public interface INotificationRepository : IRepository<Notifications, PartialNotification>
{
}