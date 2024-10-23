using System.ComponentModel.DataAnnotations;
using CleanService.Src.Models;

namespace CleanService.Src.Modules.Notification.DTOs;

public class CreateNotificationDto
{
    [Required]
    public string UserId { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Content { get; set; }
    
    [Required]
    public NotificationType Type { get; set; }
    
    public Guid? ReferenceId { get; set; }
}