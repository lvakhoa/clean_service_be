using System.Security.Claims;
using CleanService.Src.Modules.Notification.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanService.Src.Modules.Notification;

[Authorize]
[Route("[controller]")]
public class NotificationController : Controller
{
    private readonly INotificationService _notificationService;
    
    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetNotifications()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
        var notifications = await _notificationService.GetNotifications(userId);
        
        return Ok(notifications);
    }
}