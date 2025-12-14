using CleanService.Src.Models;
using CleanService.Src.Models.Enums;

namespace CleanService.Src.Constant;

public record NotificationData(string Title, string Content, NotificationType Type, Guid ReferenceId);
