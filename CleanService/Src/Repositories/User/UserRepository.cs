using CleanService.Src.Models;
using Microsoft.EntityFrameworkCore;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Repositories.User;

public class UserRepository : Repository<Users, PartialUsers>, IUserRepository
{
    private readonly CleanServiceContext _dbContext;

    public UserRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
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

    public Task<UserType> GetUserType(string userId)
    {
        return _dbContext.Users
            .Where(x => x.Id == userId)
            .Select(x => x.UserType)
            .FirstOrDefaultAsync();
    }
}