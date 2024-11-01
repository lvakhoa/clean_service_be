using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
using Microsoft.EntityFrameworkCore;
using Pagination.EntityFrameworkCore.Extensions;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CleanService.Src.Modules.Auth.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly CleanServiceContext _dbContext;

    public AuthRepository(CleanServiceContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Users?> GetUserById(string id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Users> CreateUser(Users user)
    {
        var userEntity = await _dbContext.Users.AddAsync(user);
        if (user.UserType == UserType.Helper)
        {
            await _dbContext.Helpers.AddAsync(new Models.Helpers
            {
                Id = userEntity.Entity.Id
            });
        }

        await _dbContext.SaveChangesAsync();

        return userEntity.Entity;
    }

    public async Task<Users?> UpdateInfo(string id, PartialUsers updateInfoUser)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null)
            return null;

        if (updateInfoUser.UserType is not null)
            user.UserType = updateInfoUser.UserType.Value;
        if (updateInfoUser.Gender is not null)
            user.Gender = updateInfoUser.Gender;
        if (updateInfoUser.ProfilePicture is not null)
            user.ProfilePicture = updateInfoUser.ProfilePicture;
        if (updateInfoUser.FullName is not null)
            user.FullName = updateInfoUser.FullName;
        if (updateInfoUser.DateOfBirth is not null)
            user.DateOfBirth = updateInfoUser.DateOfBirth;
        if (updateInfoUser.IdentityCard is not null)
            user.IdentityCard = updateInfoUser.IdentityCard;
        if (updateInfoUser.Address is not null)
            user.Address = updateInfoUser.Address;
        if (updateInfoUser.PhoneNumber is not null)
            user.PhoneNumber = updateInfoUser.PhoneNumber;
        if (updateInfoUser.Email is not null)
            user.Email = updateInfoUser.Email;
        if (updateInfoUser.Status is not null)
            user.Status = updateInfoUser.Status.Value;
        if (updateInfoUser.NotificationToken is not null)
            user.NotificationToken = updateInfoUser.NotificationToken;

        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<Helpers?> UpdateHelperInfo(string id, PartialHelper updateInfoHelper)
    {
        var helper = await _dbContext.Helpers.FirstOrDefaultAsync(x => x.Id == id);

        if (helper is null)
            return null;

        if (updateInfoHelper.ExperienceDescription is not null)
            helper.ExperienceDescription = updateInfoHelper.ExperienceDescription;
        if (updateInfoHelper.ResumeUploaded is not null)
            helper.ResumeUploaded = updateInfoHelper.ResumeUploaded;
        if (updateInfoHelper.ServicesOffered is not null)
            helper.ServicesOffered = updateInfoHelper.ServicesOffered;

        await _dbContext.SaveChangesAsync();

        return helper;
    }

    public async Task<Pagination<Users>> GetPagedUsersAsync(UserType? userType, int? page, int? limit, UserStatus? status = UserStatus.Active)
    {
        if (page == null || limit == null)
        {
            var allUsers = await _dbContext.Users
                .Where(x => (userType == null || x.UserType == userType) && x.Status == status)
                .OrderBy(x => x.FullName)
                .ToListAsync();
            return new Pagination<Users>(allUsers, allUsers.Count, 1, allUsers.Count);
        }

        var list = await _dbContext.Users
            .Where(x => (userType == null || x.UserType == userType) && x.Status == status)
            .OrderBy(x => x.FullName).Skip((page.Value - 1) * limit.Value)
            .Take(limit.Value)
            .ToListAsync();

        var totalItems = await _dbContext.Users
            .Where(x => (userType == null || x.UserType == userType) && x.Status == status)
            .CountAsync();

        return new Pagination<Users>(list, totalItems, page.Value, limit.Value);
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