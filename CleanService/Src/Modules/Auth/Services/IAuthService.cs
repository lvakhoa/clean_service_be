using CleanService.Src.Models;
using CleanService.Src.Models.Enums;
using CleanService.Src.Modules.Auth.Mapping.DTOs;

using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Auth.Services;

public interface IAuthService
{
    public Task RegisterUser(RegistrationRequestDto registrationRequestDto);

    public Task<SignUpMobileResponseDto> RegisterUserMobile(SignUpMobileRequestDto signUpMobileRequestDto);

    public Task<LogInMobileResponseDto> LoginUserMobile(LogInMobileRequestDto logInMobileRequestDto);

    public Task LogoutUser(string id);

    public Task<UserResponseDto?> GetUserById(string id);

    public Task UpdateInfo(string id, UpdateUserRequestDto updateUserRequestDto);

    public Task UpdateHelperInfo(string id, UpdateHelperRequestDto updateHelperRequestDto);

    public Task<Pagination<UserResponseDto>> GetUsers(UserType? userType = null, int? page = null, int? limit = null, UserStatus? status = UserStatus.Active);

    public Task ActivateUser(string id);

    public Task BlockUser(string id);

    public Task<Tokens> ExchangeCodeForTokensAsync(string code);

    public Task<UserInfo> GetUserInfoAsync(string accessToken);

    public Task<bool> CheckUserExistsAsync(string email);
}
