using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.DTOs;
using CleanService.Src.Modules.Auth.Repositories;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    
    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
    
    public async Task<UserReturnDto?> RegisterUser(RegistrationDto registrationDto)
    {
        var user = await _authRepository.GetUserById(registrationDto.Id);
        if(user == null)
            return await _authRepository.CreateUser(registrationDto);
        return null;
    }

    public async Task<UserReturnDto?> GetUserById(string id)
    {
        var user = await _authRepository.GetUserById(id);
        if(user == null)
            throw new KeyNotFoundException("User not found");
        return user;
    }
    
    public async Task<UserReturnDto?> UpdateInfo(string id, UpdateInfoDto updateInfoDto)
    {
        var user = await _authRepository.GetUserById(id);
        if(user == null)
            throw new KeyNotFoundException("User not found");
        return await _authRepository.UpdateInfo(id, updateInfoDto);
    }

    public Task<HelperReturnDto?> UpdateHelperInfo(string id, UpdateHelperDto updateHelperDto)
    {
        var helper = _authRepository.UpdateHelperInfo(id, updateHelperDto);
        if(helper == null)
            throw new KeyNotFoundException("Helper not found");
        return helper;
    }

    public Task<UserReturnDto[]> GetAllUsers(UserType? userType, UserStatus? status = UserStatus.Active)
    {
        return _authRepository.GetAllUsers(userType, status);
    }

    public Task<Pagination<UserReturnDto>> GetPagedUsersAsync(UserType? userType, UserStatus? status = UserStatus.Active, int page = 1, int limit = 1)
    {
        return _authRepository.GetPagedUsersAsync(userType, status, page, limit);
    }

    public Task<UserReturnDto?> ActivateUser(string id)
    {
        return _authRepository.UpdateInfo(id, new UpdateInfoDto
        {
            Status = UserStatus.Active
        });
    }

    public Task<UserReturnDto?> BlockUser(string id)
    {
        return _authRepository.UpdateInfo(id, new UpdateInfoDto
        {
            Status = UserStatus.Blocked
        });
    }
}