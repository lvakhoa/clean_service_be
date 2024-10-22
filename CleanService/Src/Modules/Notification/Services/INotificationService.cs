using CleanService.Src.Constant;
using CleanService.Src.Modules.Notification.DTOs;

namespace CleanService.Src.Modules.Notification.Services;

public interface INotificationService
{
    public Task SendSpecificUser(string userId, NotificationData message);
    
    public Task SendMultipleUsers(List<string> userIds, NotificationData message);

    public Task<NotificationReturnDto[]> GetNotifications(string userId);
}