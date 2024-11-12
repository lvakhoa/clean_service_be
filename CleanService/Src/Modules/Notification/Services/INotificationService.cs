using CleanService.Src.Constant;
using CleanService.Src.Modules.Notification.Mapping.DTOs;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Notification.Services;

public interface INotificationService
{
    public Task SendSpecificUser(string userId, NotificationData message);
    
    public Task SendMultipleUsers(List<string> userIds, NotificationData message);

    public Task<Pagination<NotificationResponseDto>> GetNotifications(string userId, int? page, int? limit);
}