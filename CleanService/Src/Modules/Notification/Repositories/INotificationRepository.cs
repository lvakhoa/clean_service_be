using CleanService.Src.Modules.Notification.DTOs;

namespace CleanService.Src.Modules.Notification.Repositories;

public interface INotificationRepository
{
    public Task<NotificationReturnDto> CreateNotification(CreateNotificationDto data);
    
    public Task<NotificationReturnDto[]> GetNotifications(string userId);
}