using System.Linq.Expressions;
using System.Net.Http.Headers;
using AutoMapper;
using CleanService.Src.Constant;
using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Infrastructures;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
using CleanService.Src.Modules.Storage.Services;
using CleanService.Src.Repositories;
using CleanService.Src.Utils.RequestClient;
using Newtonsoft.Json;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IRequestClient _requestClient;
    private readonly IConfiguration _configuration;
    private readonly IAuthUnitOfWork _authUnitOfWork;
    private readonly IMapper _mapper;
    private readonly IStorageService _storageService;

    public AuthService(IRequestClient requestClient, IConfiguration configuration, IAuthUnitOfWork authUnitOfWork,
        IMapper mapper, IStorageService storageService)
    {
        _requestClient = requestClient;
        _configuration = configuration;
        _authUnitOfWork = authUnitOfWork;
        _mapper = mapper;
        _storageService = storageService;
    }

    public async Task RegisterUser(RegistrationRequestDto registrationRequestDto)
    {
        var user = await _authUnitOfWork.UserRepository.FindOneAsync(entity => entity.Id == registrationRequestDto.Id,
            new FindOptions
            {
                IsAsNoTracking = true
            });

        if (user == null)
        {
            var userEntity = _mapper.Map<Users>(registrationRequestDto);
            await _authUnitOfWork.UserRepository.AddAsync(userEntity);

            if (userEntity.UserType == UserType.Helper)
            {
                await _authUnitOfWork.HelperRepository.AddAsync(new Helpers
                {
                    Id = userEntity.Id
                });
            }

            await _authUnitOfWork.SaveChangesAsync();
        }
    }

    public async Task LogoutUser(string id)
    {
        var secretKey = _configuration.GetValue<string>("OAuthProvider:SecretKey");
        // Get all user's sessions
        var sessions = await _requestClient.GetJson<List<Dictionary<string, object?>>>(
            $"{RemoteBaseUrl.ClerkBaseUrl}/sessions?user_id={id}&status=active", new Dictionary<string, object>
            {
                { "Authorization", $"Bearer {secretKey}" }
            });

        foreach (var s in sessions)
        {
            await _requestClient.PostFormAsync($"{RemoteBaseUrl.ClerkBaseUrl}/sessions/{s["id"]}/revoke",
                new Dictionary<string, string>(), headers: new Dictionary<string, object>
                {
                    { "Authorization", $"Bearer {secretKey}" }
                });
        }
    }


    public async Task<UserResponseDto?> GetUserById(string id)
    {
        var user = await _authUnitOfWork.UserRepository.FindOneAsync(entity => entity.Id == id,
            new FindOptions
            {
                IsAsNoTracking = true
            });
        if (user == null)
            throw new KeyNotFoundException("User not found");

        var userDto = _mapper.Map<UserResponseDto>(user);

        return userDto;
    }

    public async Task UpdateInfo(string id, UpdateUserRequestDto updateUserRequestDto)
    {
        var user = await _authUnitOfWork.UserRepository.FindOneAsync(entity => entity.Id == id, new FindOptions
        {
            IsAsNoTracking = true,
            IsIgnoreAutoIncludes = true
        });
        if (user == null)
            throw new KeyNotFoundException("User not found");
        _authUnitOfWork.UserRepository.Detach(user);

        if (user.ProfilePicture != null && updateUserRequestDto.ProfilePicture != null)
        {
            await _storageService.DeleteFileAsync(user.ProfilePicture);
        }

        if (user.IdentityCard != null && updateUserRequestDto.IdentityCard != null)
        {
            await _storageService.DeleteFileAsync(user.IdentityCard);
        }

        var userEntity = _mapper.Map<PartialUsers>(updateUserRequestDto);
        _authUnitOfWork.UserRepository.Update(userEntity, user);

        await _authUnitOfWork.SaveChangesAsync();
    }

    public async Task UpdateHelperInfo(string id, UpdateHelperRequestDto updateHelperRequestDto)
    {
        var helper = await _authUnitOfWork.HelperRepository.FindOneAsync(entity => entity.Id == id, new FindOptions
        {
            IsAsNoTracking = true,
            IsIgnoreAutoIncludes = true
        });
        if (helper == null)
            throw new KeyNotFoundException("User not found");
        _authUnitOfWork.HelperRepository.Detach(helper);

        if (helper.ResumeUploaded != null && updateHelperRequestDto.ResumeUploaded != null)
        {
            await _storageService.DeleteFileAsync(helper.ResumeUploaded);
        }

        var helperEntity = _mapper.Map<PartialHelper>(updateHelperRequestDto);
        _authUnitOfWork.HelperRepository.Update(helperEntity, helper);

        await _authUnitOfWork.SaveChangesAsync();
    }

    public async Task<Pagination<UserResponseDto>> GetUsers(UserType? userType, int? page, int? limit,
        UserStatus? status = UserStatus.Active)
    {
        Expression<Func<Users, bool>> predicate = userType == null
            ? entity => entity.Status == status
            : entity => entity.UserType == userType && entity.Status == status;

        var users = _authUnitOfWork.UserRepository.Find(predicate,
            order: entity => entity.FullName, false, page, limit,
            new FindOptions
            {
                IsAsNoTracking = true
            });
        var totalUsers = await _authUnitOfWork.UserRepository.CountAsync(predicate);

        var userDto = _mapper.Map<UserResponseDto[]>(users);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalUsers;

        return new Pagination<UserResponseDto>(userDto, totalUsers,
            currentPage,
            currentLimit);
    }

    public async Task ActivateUser(string id)
    {
        var user = await _authUnitOfWork.UserRepository.FindOneAsync(entity => entity.Id == id);
        if (user == null)
            throw new KeyNotFoundException("User not found");

        user.Status = UserStatus.Active;

        await _authUnitOfWork.SaveChangesAsync();
    }

    public async Task BlockUser(string id)
    {
        var user = await _authUnitOfWork.UserRepository.FindOneAsync(entity => entity.Id == id);
        if (user == null)
            throw new KeyNotFoundException("User not found");

        user.Status = UserStatus.Blocked;

        await _authUnitOfWork.SaveChangesAsync();
    }

    public async Task<Tokens> ExchangeCodeForTokensAsync(string code)
    {
        var providerDomain = _configuration.GetValue<string>("OAuthProvider:Domain");

        return await _requestClient.PostFormAsync<Tokens>(AuthProvider.TokenEndpoint(providerDomain!),
            new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", _configuration.GetValue<string>("OAuthProvider:ClientId")! },
                { "client_secret", _configuration.GetValue<string>("OAuthProvider:ClientSecret")! },
                { "grant_type", "authorization_code" },
                { "redirect_uri", "http://localhost:5011/api/v1/oauth/callback" }
            });
    }

    public async Task<UserInfo> GetUserInfoAsync(string accessToken)
    {
        var providerDomain = _configuration.GetValue<string>("OAuthProvider:Domain");

        return await _requestClient.GetJson<UserInfo>(AuthProvider.UserInformationEndpoint(providerDomain!),
            new Dictionary<string, object>
            {
                { "Bearer", accessToken }
            });
    }

    public async Task<bool> CheckUserExistsAsync(string email)
    {
        var response =
            await _requestClient.GetJson<List<User>>($"{RemoteBaseUrl.ClerkBaseUrl}/users?email_address={email}");

        return response.Any();
    }
}