using CleanService.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Repositories.Notification;

public class NotificationRepository : Repository<Notifications, PartialNotification>, INotificationRepository
{
    private readonly CleanServiceContext _dbContext;
    
    public NotificationRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}