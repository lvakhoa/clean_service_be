using CleanService.Src.Constant;
using CleanService.Src.Modules.Auth.Repositories;
using CleanService.Src.Modules.Notification.DTOs;
using CleanService.Src.Modules.Notification.Repositories;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;

namespace CleanService.Src.Modules.Notification.Services;

public class FirebaseService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;

    private readonly IAuthRepository _authRepository;

    private readonly FirebaseMessaging _message;

    public FirebaseService(INotificationRepository notificationRepository, IAuthRepository authRepository,
        IConfiguration config)
    {
        _notificationRepository = notificationRepository;

        _authRepository = authRepository;

        var settings = config
            .GetSection("FirebaseConfig")
            .Get<Dictionary<string, string>>();
        var json = JsonConvert.SerializeObject(settings);

        var app = FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromJson(json)
        });

        _message = FirebaseMessaging.GetMessaging(app);
    }

    public async Task SendSpecificUser(string userId, NotificationData data)
    {
        var user = await _authRepository.GetUserById(userId);

        await _notificationRepository.CreateNotification(new CreateNotificationDto
        {
            Title = data.Title,
            Content = data.Content,
            UserId = userId,
            Type = data.Type,
            ReferenceId = data.ReferenceId
        });

        var message = new Message
        {
            Data = data.GetType().GetProperties()
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(data) as string),
            Token = user?.NotificationToken
        };
        await _message.SendAsync(message);
    }

    public async Task SendMultipleUsers(List<string>? userIds, NotificationData data)
    {
        var notificationToken = await _authRepository.GetUserNotificationTokens(userIds);
        
        if (userIds is not null)
        {
            foreach (var id in userIds)
            {
                await _notificationRepository.CreateNotification(new CreateNotificationDto
                {
                    Title = data.Title,
                    Content = data.Content,
                    UserId = id,
                    Type = data.Type,
                    ReferenceId = data.ReferenceId
                });
            }
        }

        var message = new MulticastMessage()
        {
            Data = data.GetType().GetProperties()
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(data) as string),
            Tokens = notificationToken
        };
        await _message.SendEachForMulticastAsync(message);
    }
    
    public async Task<NotificationReturnDto[]> GetNotifications(string userId)
    {
        return await _notificationRepository.GetNotifications(userId);
    }
}