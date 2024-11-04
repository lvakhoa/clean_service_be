using CleanService.Src.Models;

namespace CleanService.Src.Modules.Notification.DTOs;

public class NotificationResponseDto
{
    public string Id { get; set; }

    public string UserId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;
    
    public string Type { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    
    public string? ReferenceId { get; set; }
    
    public bool IsRead { get; set; }
}