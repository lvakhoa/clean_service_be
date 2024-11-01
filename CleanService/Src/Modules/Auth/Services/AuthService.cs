using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
using CleanService.Src.Modules.Auth.Repositories;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    
    private readonly IMapper _mapper;

    public AuthService(IAuthRepository authRepository, IMapper mapper)
    {
        _authRepository = authRepository;
        _mapper = mapper;
    }
    
    public async Task<UserReturnDto?> RegisterUser(RegistrationDto registrationDto)
    {
        var user = await _authRepository.GetUserById(registrationDto.Id);
        if (user == null)
        {
            var userEntity = _mapper.Map<Users>(registrationDto);
            var createdUser = await _authRepository.CreateUser(userEntity);
            var userDto = _mapper.Map<UserReturnDto>(createdUser);
            return userDto;
        }
        return null;
    }

    public async Task<UserReturnDto?> GetUserById(string id)
    {
        var user = await _authRepository.GetUserById(id);
        if(user == null)
            throw new KeyNotFoundException("User not found");
        
        var userDto = _mapper.Map<UserReturnDto>(user);
        
        return userDto;
    }
    
    public async Task<UserReturnDto?> UpdateInfo(string id, UpdateInfoDto updateInfoDto)
    {
        var user = await _authRepository.GetUserById(id);
        if(user == null)
            throw new KeyNotFoundException("User not found");
        
        var userEntity = _mapper.Map<PartialUsers>(updateInfoDto);
        var updatedUser = await _authRepository.UpdateInfo(id, userEntity);
        
        var userDto = _mapper.Map<UserReturnDto>(updatedUser);
        return userDto;
    }

    public async Task<HelperReturnDto?> UpdateHelperInfo(string id, UpdateHelperDto updateHelperDto)
    {
        var helperEntity = _mapper.Map<PartialHelper>(updateHelperDto);
        var helper = await _authRepository.UpdateHelperInfo(id, helperEntity);
        if(helper == null)
            throw new KeyNotFoundException("Helper not found");
        var helperDto = _mapper.Map<HelperReturnDto>(helper);
        return helperDto;
    }

    public async Task<Pagination<UserReturnDto>> GetPagedUsersAsync(UserType? userType, int? page, int? limit, UserStatus? status = UserStatus.Active)
    {
        var users = await _authRepository.GetPagedUsersAsync(userType, page, limit, status);
        var response = _mapper.Map<UserReturnDto[]>(users.Results);

        var currentPage = page ?? 1;
        var currentLimit = (int)(limit ?? users.TotalItems);

        return new Pagination<UserReturnDto>(response, users.TotalItems,
            currentPage,
            currentLimit);
    }
    
    public async Task<UserReturnDto?> ActivateUser(string id)
    {
        var user = await _authRepository.UpdateInfo(id, new PartialUsers()
        {
            Status = UserStatus.Active
        });
        
        var userDto = _mapper.Map<UserReturnDto>(user);
        return userDto;
    }

    public async Task<UserReturnDto?> BlockUser(string id)
    {
        var user = await _authRepository.UpdateInfo(id, new PartialUsers
        {
            Status = UserStatus.Blocked
        });
        
        var userDto = _mapper.Map<UserReturnDto>(user);
        return userDto;
    }
}