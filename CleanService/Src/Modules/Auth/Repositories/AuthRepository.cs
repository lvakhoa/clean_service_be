using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.DTOs;
using Microsoft.EntityFrameworkCore;
using Pagination.EntityFrameworkCore.Extensions;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace CleanService.Src.Modules.Auth.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly CleanServiceContext _dbContext;

    public AuthRepository(CleanServiceContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserReturnDto?> GetUserById(string id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

        return user is not null
            ? new UserReturnDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                UserType = user.UserType.ToString(),
                NotificationToken = user.NotificationToken,
                CreatedAt = user.CreatedAt
            }
            : null;
    }

    public async Task<UserReturnDto> CreateUser(RegistrationDto registration)
    {
        var userEntity = await _dbContext.Users.AddAsync(new Users
        {
            Id = registration.Id,
            FullName = registration.Fullname,
            Email = registration.Email,
            UserType = registration.UserType
        });
        if (registration.UserType == UserType.Helper)
        {
            await _dbContext.Helpers.AddAsync(new Models.Helpers
            {
                Id = userEntity.Entity.Id
            });
        }

        await _dbContext.SaveChangesAsync();

        return new UserReturnDto
        {
            Id = userEntity.Entity.Id,
            FullName = userEntity.Entity.FullName,
            Email = userEntity.Entity.Email,
            PhoneNumber = userEntity.Entity.PhoneNumber,
            Address = userEntity.Entity.Address,
            UserType = userEntity.Entity.UserType.ToString(),
            NotificationToken = userEntity.Entity.NotificationToken,
            CreatedAt = userEntity.Entity.CreatedAt
        };
    }

    public async Task<UserReturnDto?> UpdateInfo(string id, UpdateInfoDto updateInfoDto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null)
            return null;

        if (updateInfoDto.FullName is not null)
            user.FullName = updateInfoDto.FullName;
        if (updateInfoDto.Address is not null)
            user.Address = updateInfoDto.Address;
        if (updateInfoDto.PhoneNumber is not null)
            user.PhoneNumber = updateInfoDto.PhoneNumber;
        if (updateInfoDto.Status is not null)
            user.Status = updateInfoDto.Status.Value;

        await _dbContext.SaveChangesAsync();
        return new UserReturnDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            UserType = user.UserType.ToString(),
            NotificationToken = user.NotificationToken,
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<HelperReturnDto?> UpdateHelperInfo(string id, UpdateHelperDto updateHelperDto)
    {
        var helper = await _dbContext.Helpers.FirstOrDefaultAsync(x => x.Id == id);

        if (helper is null)
            return null;

        if (updateHelperDto.ExperienceDescription is not null)
            helper.ExperienceDescription = updateHelperDto.ExperienceDescription;
        // if (updateHelperDto.ServicesOffered is not null)
        //     helper.ServicesOffered = updateHelperDto.ServicesOffered;

        await _dbContext.SaveChangesAsync();

        return new HelperReturnDto
        {
            Id = helper.Id,
            ExperienceDescription = helper.ExperienceDescription,
            AverageRating = helper.AverageRating,
            CompletedJobs = helper.CompletedJobs,
            CancelledJobs = helper.CancelledJobs
        };
    }

    public async Task<UserReturnDto[]> GetAllUsers(UserType? userType, UserStatus? status = UserStatus.Active)
    {
        var users = await _dbContext.Users
            .Where(x => (userType == null || x.UserType == userType) && x.Status == status)
            .ToArrayAsync();

        return users.Select(x => new UserReturnDto
        {
            Id = x.Id,
            FullName = x.FullName,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber,
            Address = x.Address,
            UserType = x.UserType.ToString(),
            NotificationToken = x.NotificationToken,
            CreatedAt = x.CreatedAt
        }).ToArray();
    }

    public async Task<Pagination<UserReturnDto>> GetPagedUsersAsync(UserType? userType, UserStatus? status = UserStatus.Active, int page = 1, int limit = 1)
    {

        var list = await _dbContext.Users
            .Where(x => (userType == null || x.UserType == userType) && x.Status == status).OrderBy(x => x.FullName).Skip((page - 1) * limit).Take(limit)
            .ToListAsync();

        var totalItems = await _dbContext.Users.Where(x => (userType == null || x.UserType == userType) && x.Status == status).CountAsync();

        var users = list.Select(x => new UserReturnDto
        {
            Id = x.Id,
            FullName = x.FullName,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber,
            Address = x.Address,
            UserType = x.UserType.ToString(),
            NotificationToken = x.NotificationToken,
            CreatedAt = x.CreatedAt
        });

        return new Pagination<UserReturnDto>(users, totalItems, page, limit);
    }

    public Task<string?[]> GetUserNotificationTokens(List<string>? userIds)
    {
        if (userIds is null)
            return _dbContext.Users
                .Select(x => x.NotificationToken)
                .ToArrayAsync();

        return _dbContext.Users
            .Where(x => userIds.Contains(x.Id))
            .Select(x => x.NotificationToken)
            .ToArrayAsync();
    }
}