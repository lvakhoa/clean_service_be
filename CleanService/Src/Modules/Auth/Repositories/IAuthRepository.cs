using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.DTOs;

namespace CleanService.Src.Modules.Auth.Repositories;

public interface IAuthRepository
{
    public Task<UserReturnDto?> GetUserById(string id);
    
    public Task<UserReturnDto> CreateUser(RegistrationDto registration);
    
    public Task<UserReturnDto?> UpdateInfo(string id, UpdateInfoDto updateInfoDto);
    
    public Task<HelperReturnDto?> UpdateHelperInfo(string id, UpdateHelperDto updateHelperDto);
    
    public Task<UserReturnDto[]> GetAllUsers(UserType? userType, UserStatus? status = UserStatus.Active);
    
    public Task<string?[]> GetUserNotificationTokens(List<string>? userIds);
}