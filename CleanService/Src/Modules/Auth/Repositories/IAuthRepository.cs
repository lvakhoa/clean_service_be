using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Auth.Repositories;

public interface IAuthRepository
{
    public Task<Users?> GetUserById(string id);
    
    public Task<Users> CreateUser(Users user);
    
    public Task<Users?> UpdateInfo(string id, PartialUsers updateInfoUser);
    
    public Task<Helpers?> UpdateHelperInfo(string id, PartialHelper updateInfoHelper);
   
    public Task<Pagination<Users>> GetPagedUsersAsync(UserType? userType = null, int? page = null, int? limit = null, UserStatus? status = UserStatus.Active);
    
    public Task<string?[]> GetUserNotificationTokens(List<string>? userIds);
}