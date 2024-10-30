using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;

namespace CleanService.Src.Modules.Auth.Repositories;

public interface IAuthRepository
{
    public Task<Users?> GetUserById(string id);
    
    public Task<Users> CreateUser(Users user);
    
    public Task<Users?> UpdateInfo(string id, PartialUsers updateInfoUser);
    
    public Task<Helpers?> UpdateHelperInfo(string id, PartialHelper updateInfoHelper);
    
    public Task<Users[]> GetAllUsers(UserType? userType, UserStatus? status = UserStatus.Active);
    
    public Task<string?[]> GetUserNotificationTokens(List<string>? userIds);
}