using System.Net;
using System.Security.Claims;
using CleanService.Src.Constant;
using CleanService.Src.Modules.Notification.Services;
using CleanService.Src.Utils;
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

    [HttpGet("all")]
    public async Task<IActionResult> GetNotifications(int? page, int? limit)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            throw new ExceptionResponse(HttpStatusCode.Unauthorized, "Unauthorized", ExceptionConvention.Unauthorized);

        var notifications = await _notificationService.GetNotifications(userId, page, limit);

        return Ok(new SuccessResponse
        {
            StatusCode = HttpStatusCode.OK,
            Message = "Get all notifications successfully",
            Data = notifications
        });
    }
}