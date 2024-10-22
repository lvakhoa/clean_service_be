using CleanService.Src.Models;

namespace CleanService.Src.Modules.Notification.DTOs;

public class NotificationReturnDto
{
    public Guid Id { get; set; }
    
    public string UserId { get; set; }
    
    public string Title { get; set; }
    
    public string Content { get; set; }
    
    public NotificationType Type { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public Guid? ReferenceId { get; set; }
    
    public bool IsRead { get; set; }
}