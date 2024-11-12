using AutoMapper;
using CleanService.Src.Constant;
using CleanService.Src.Models;
using CleanService.Src.Modules.Notification.Infrastructures;
using CleanService.Src.Modules.Notification.Mapping.DTOs;
using CleanService.Src.Repositories;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Notification.Services;

public class FirebaseService : INotificationService
{
    private readonly INotificationUnitOfWork _notificationUnitOfWork;

    private readonly IMapper _mapper;

    private readonly FirebaseMessaging _message;

    public FirebaseService(INotificationUnitOfWork notificationUnitOfWork, IMapper mapper,
        IConfiguration config)
    {
        _notificationUnitOfWork = notificationUnitOfWork;

        _mapper = mapper;

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
        var user = await _notificationUnitOfWork.UserRepository.FindOneAsync(entity => entity.Id == userId,
            new FindOptions
            {
                IsAsNoTracking = true,
                IsIgnoreAutoIncludes = true
            });
        if (user is null)
            throw new Exception("User not found");

        await _notificationUnitOfWork.NotificationRepository.AddAsync(new Notifications()
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
            Token = user.NotificationToken
        };
        await _message.SendAsync(message);

        await _notificationUnitOfWork.SaveChangesAsync();
    }

    public async Task SendMultipleUsers(List<string>? userIds, NotificationData data)
    {
        var notificationToken = await _notificationUnitOfWork.UserRepository.GetUserNotificationTokens(userIds);

        if (userIds is not null)
        {
            foreach (var id in userIds)
            {
                await _notificationUnitOfWork.NotificationRepository.AddAsync(new Notifications()
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

        await _notificationUnitOfWork.SaveChangesAsync();
    }

    public async Task<Pagination<NotificationResponseDto>> GetNotifications(string userId, int? page, int? limit)
    {
        var notifications = _notificationUnitOfWork.NotificationRepository.Find(entity => entity.UserId == userId,
            order: null, false, page, limit, new FindOptions
            {
                IsAsNoTracking = true
            });
        var totalNotifications = await _notificationUnitOfWork.NotificationRepository.CountAsync();

        var notificationDtos = _mapper.Map<NotificationResponseDto[]>(notifications);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalNotifications;

        return new Pagination<NotificationResponseDto>(notificationDtos, totalNotifications,
            currentPage,
            currentLimit);
    }
}