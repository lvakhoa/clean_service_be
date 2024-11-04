using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;

using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Auth.Services;

public interface IAuthService
{
    public Task RegisterUser(RegistrationRequestDto registrationRequestDto);
    
    public Task<UserResponseDto?> GetUserById(string id);
    
    public Task UpdateInfo(string id, UpdateUserRequestDto updateUserRequestDto);
    
    public Task UpdateHelperInfo(string id, UpdateHelperRequestDto updateHelperRequestDto);

    public Task<Pagination<UserResponseDto>> GetUsers(UserType? userType = null, int? page = null, int? limit = null, UserStatus? status = UserStatus.Active);

    public Task ActivateUser(string id);
    
    public Task BlockUser(string id);
}