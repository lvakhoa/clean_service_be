using System.Linq.Expressions;
using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Infrastructures;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
using CleanService.Src.Repositories;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Modules.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IAuthUnitOfWork _authUnitOfWork;

    private readonly IMapper _mapper;

    public AuthService(IAuthUnitOfWork authUnitOfWork, IMapper mapper)
    {
        _authUnitOfWork = authUnitOfWork;
        _mapper = mapper;
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
            order: entity => entity.FullName, page, limit,
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
}