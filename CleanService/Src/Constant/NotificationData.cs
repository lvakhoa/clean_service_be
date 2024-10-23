using CleanService.Src.Models;

namespace CleanService.Src.Constant;

public record NotificationData(string Title, string Content, NotificationType Type, Guid ReferenceId);