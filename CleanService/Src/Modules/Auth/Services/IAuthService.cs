using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.DTOs;

namespace CleanService.Src.Modules.Auth.Services;

public interface IAuthService
{
    public Task<UserReturnDto?> RegisterUser(RegistrationDto registrationDto);
    
    public Task<UserReturnDto?> GetUserById(string id);
    
    public Task<UserReturnDto?> UpdateInfo(string id, UpdateInfoDto updateInfoDto);
    
    public Task<HelperReturnDto?> UpdateHelperInfo(string id, UpdateHelperDto updateHelperDto);
    
    public Task<UserReturnDto[]> GetAllUsers(UserType? userType, UserStatus? status = UserStatus.Active);
    
    public Task<UserReturnDto?> ActivateUser(string id);
    
    public Task<UserReturnDto?> BlockUser(string id);
}