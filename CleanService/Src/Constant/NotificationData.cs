using CleanService.Src.Models;
using CleanService.Src.Models.Configurations;

namespace CleanService.Src.Constant;

public record NotificationData(string Title, string Content, NotificationType Type, Guid ReferenceId);
