using CleanService.Src.Models;
using CleanService.Src.Modules.Notification.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Repositories.Notification;

public class NotificationRepository : Repository<Notifications>, INotificationRepository
{
    private readonly CleanServiceContext _dbContext;
    
    public NotificationRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}