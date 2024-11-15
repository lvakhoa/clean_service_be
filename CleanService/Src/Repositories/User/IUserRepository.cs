using CleanService.Src.Models;

namespace CleanService.Src.Repositories.User;

public interface IUserRepository : IRepository<Users, PartialUsers>
{
    public Task<string?[]> GetUserNotificationTokens(List<string>? userIds);
    
    public Task<UserType> GetUserType(string userId);
}