using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Notification.Mapping.DTOs;

namespace CleanService.Src.Modules.Notification.Mapping.Profiles;

public class NotificationResponseProfile : Profile
{
    public NotificationResponseProfile()
    {
        CreateMap<Notifications, NotificationResponseDto>()
            .ConstructUsing(entity => new NotificationResponseDto
            {
                Id = entity.Id.ToString(),
                UserId = entity.UserId,
                Title = entity.Title,
                Content = entity.Content,
                Type = entity.Type.ToString(),
                CreatedAt = entity.CreatedAt,
                ReferenceId = entity.ReferenceId.ToString(),
                IsRead = entity.IsRead,
            });
    }
}