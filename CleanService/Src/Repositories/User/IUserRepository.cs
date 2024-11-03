using CleanService.Src.Models;

namespace CleanService.Src.Repositories.User;

public interface IUserRepository : IRepository<Users>
{
    public Task<string?[]> GetUserNotificationTokens(List<string>? userIds);
}