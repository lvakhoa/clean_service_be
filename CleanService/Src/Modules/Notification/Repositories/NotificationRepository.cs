using CleanService.Src.Models;
using CleanService.Src.Modules.Notification.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Notification.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly CleanServiceContext _dbContext;
    
    public NotificationRepository(CleanServiceContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<NotificationReturnDto> CreateNotification(CreateNotificationDto data)
    {
        var notification = await _dbContext.Notifications.AddAsync(new Notifications
        {
            Title = data.Title,
            Content = data.Content,
            Type = data.Type,
            UserId = data.UserId,
            ReferenceId = data.ReferenceId
        });
        
        await _dbContext.SaveChangesAsync();
        
        return new NotificationReturnDto
        {
            Id = notification.Entity.Id,
            Title = notification.Entity.Title,
            Content = notification.Entity.Content,
            Type = notification.Entity.Type,
            UserId = notification.Entity.UserId,
            ReferenceId = notification.Entity.ReferenceId,
            CreatedAt = notification.Entity.CreatedAt,
            IsRead = notification.Entity.IsRead
        };
    }

    public async Task<NotificationReturnDto[]> GetNotifications(string userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user is null)
        {
            throw new Exception("User not found");
        }
        
        var notifications = await _dbContext.Notifications.Where(x => x.UserId == userId).ToListAsync();
        
        return notifications.Select(x => new NotificationReturnDto
        {
            Id = x.Id,
            Title = x.Title,
            Content = x.Content,
            Type = x.Type,
            UserId = x.UserId,
            ReferenceId = x.ReferenceId,
            CreatedAt = x.CreatedAt,
            IsRead = x.IsRead
        }).ToArray();
    }
}