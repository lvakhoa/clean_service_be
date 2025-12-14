using AutoMapper;

using CleanService.Src.Constant;
using CleanService.Src.Infrastructures.Repositories;
using CleanService.Src.Infrastructures.Specifications.Impl;
using CleanService.Src.Models.Domains;
using CleanService.Src.Models.Enums;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
using CleanService.Src.Modules.Storage.Services;
using CleanService.Src.Utils.RequestClient;

using Newtonsoft.Json;

using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IRequestClient _requestClient;
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStorageService _storageService;

    public AuthService(IRequestClient requestClient, IConfiguration configuration, IUnitOfWork unitOfWork,
        IMapper mapper, IStorageService storageService)
    {
        _requestClient = requestClient;
        _configuration = configuration;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _storageService = storageService;
    }

    public async Task RegisterUser(RegistrationRequestDto registrationRequestDto)
    {
        var userSpec = UserSpecification.GetUserByIdSpec(registrationRequestDto.Id);
        var user = await _unitOfWork.Repository<Users, PartialUsers>().GetFirstAsync(userSpec);

        if (user == null)
        {
            var userEntity = _mapper.Map<Users>(registrationRequestDto);
            await _unitOfWork.Repository<Users, PartialUsers>().AddAsync(userEntity);

            if (userEntity.UserType == UserType.Helper)
            {
                await _unitOfWork.Repository<Helpers, PartialHelper>().AddAsync(new Helpers { Id = userEntity.Id });
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task LogoutUser(string id)
    {
        var secretKey = _configuration.GetValue<string>("OAuthProvider:SecretKey");
        // Get all user's sessions
        var sessions = await _requestClient.GetJson<List<Dictionary<string, object?>>>(
            $"{RemoteBaseUrl.ClerkBaseUrl}/sessions?user_id={id}&status=active",
            new Dictionary<string, object> { { "Authorization", $"Bearer {secretKey}" } });

        foreach (var s in sessions)
        {
            await _requestClient.PostFormAsync($"{RemoteBaseUrl.ClerkBaseUrl}/sessions/{s["id"]}/revoke",
                new Dictionary<string, string>(),
                headers: new Dictionary<string, object> { { "Authorization", $"Bearer {secretKey}" } });
        }
    }


    public async Task<UserResponseDto?> GetUserById(string id)
    {
        var userSpec = UserSpecification.GetUserByIdSpec(id);
        return await _unitOfWork.Repository<Users, PartialUsers>().GetFirstOrThrowAsync(userSpec)
            .ContinueWith(u => _mapper.Map<UserResponseDto>(u.Result));
    }

    public async Task UpdateInfo(string id, UpdateUserRequestDto updateUserRequestDto)
    {
        var userSpec = UserSpecification.GetUserByIdSpec(id);
        var user = await _unitOfWork.Repository<Users, PartialUsers>().GetFirstAsync(userSpec);
        if (user == null) throw new KeyNotFoundException("User not found");
        await _unitOfWork.Repository<Users, PartialUsers>().Detach(user);

        if (user.ProfilePicture != null && updateUserRequestDto.ProfilePicture != null)
        {
            await _storageService.DeleteFileAsync(user.ProfilePicture);
        }

        if (user.IdentityCard != null && updateUserRequestDto.IdentityCard != null)
        {
            await _storageService.DeleteFileAsync(user.IdentityCard);
        }

        var userEntity = _mapper.Map<PartialUsers>(updateUserRequestDto);
        await _unitOfWork.Repository<Users, PartialUsers>().UpdateAsync(userEntity, user);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateHelperInfo(string id, UpdateHelperRequestDto updateHelperRequestDto)
    {
        var helperSpec = HelperSpecification.GetHelperByIdSpec(id);
        var helper = await _unitOfWork.Repository<Helpers, PartialHelper>().GetFirstAsync(helperSpec);
        if (helper == null) throw new KeyNotFoundException("Helper not found");
        await _unitOfWork.Repository<Helpers, PartialHelper>().Detach(helper);

        if (helper.ResumeUploaded != null && updateHelperRequestDto.ResumeUploaded != null)
        {
            await _storageService.DeleteFileAsync(helper.ResumeUploaded);
        }

        var helperEntity = _mapper.Map<PartialHelper>(updateHelperRequestDto);
        await _unitOfWork.Repository<Helpers, PartialHelper>().UpdateAsync(helperEntity, helper);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Pagination<UserResponseDto>> GetUsers(UserType? userType, int? page, int? limit,
        UserStatus? status = UserStatus.Active)
    {
        var userSpec = UserSpecification.GetUserByStatusOrTypeSpec(userType, status);
        if (page.HasValue && limit.HasValue)
        {
            userSpec.ApplyPaging((page.Value - 1) * limit.Value, limit.Value);
        }
        userSpec.ApplyOrderBy(x => x.FullName);

        var users = _unitOfWork.Repository<Users, PartialUsers>().GetAllAsync(userSpec);
        var totalUsers = await _unitOfWork.Repository<Users, PartialUsers>().CountAsync(userSpec);

        var userDto = _mapper.Map<UserResponseDto[]>(users);

        var currentPage = page ?? 1;
        var currentLimit = limit ?? totalUsers;

        return new Pagination<UserResponseDto>(userDto, totalUsers, currentPage, currentLimit);
    }

    public async Task ActivateUser(string id)
    {
        var userSpec = UserSpecification.GetUserByIdSpec(id);
        var user = await _unitOfWork.Repository<Users, PartialUsers>().GetFirstOrThrowAsync(userSpec);
        user.Status = UserStatus.Active;
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task BlockUser(string id)
    {
        var userSpec = UserSpecification.GetUserByIdSpec(id);
        var user = await _unitOfWork.Repository<Users, PartialUsers>().GetFirstOrThrowAsync(userSpec);
        user.Status = UserStatus.Blocked;
        await _unitOfWork.SaveChangesAsync();
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
            new Dictionary<string, object> { { "Bearer", accessToken } });
    }

    public async Task<bool> CheckUserExistsAsync(string email)
    {
        var response =
            await _requestClient.GetJson<List<User>>($"{RemoteBaseUrl.ClerkBaseUrl}/users?email_address={email}");

        return response.Any();
    }
}
