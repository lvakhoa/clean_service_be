using CleanService.Src.Models;
using CleanService.Src.Repositories.Notification;
using CleanService.Src.Repositories.User;

namespace CleanService.Src.Modules.Notification.Infrastructures;

public class NotificationUnitOfWork : INotificationUnitOfWork
{
    private readonly CleanServiceContext _dbContext;

    public INotificationRepository NotificationRepository { get; }
    
    public IUserRepository UserRepository { get; }

    public NotificationUnitOfWork(CleanServiceContext dbContext,
        INotificationRepository notificationRepository, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        NotificationRepository = notificationRepository;
        UserRepository = userRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}