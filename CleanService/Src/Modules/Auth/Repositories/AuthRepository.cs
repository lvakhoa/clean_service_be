using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Auth.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly CleanServiceContext _dbContext;

    public AuthRepository(CleanServiceContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserReturnDto?> GetUserById(string id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == id);

        return user is not null
            ? new UserReturnDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                UserType = user.UserType
            }
            : null;
    }

    public async Task<UserReturnDto> CreateUser(RegistrationDto registration)
    {
        var userEntity = await _dbContext.Users.AddAsync(new Users
        {
            Id = registration.Id,
            FullName = registration.Fullname,
            Email = registration.Email,
            UserType = registration.UserType
        });
        if (registration.UserType == UserType.Helper)
        {
            await _dbContext.Helpers.AddAsync(new Models.Helpers
            {
                Id = userEntity.Entity.Id
            });
        }

        await _dbContext.SaveChangesAsync();

        return new UserReturnDto
        {
            Id = userEntity.Entity.Id,
            FullName = userEntity.Entity.FullName,
            Email = userEntity.Entity.Email,
            PhoneNumber = userEntity.Entity.PhoneNumber,
            Address = userEntity.Entity.Address,
            UserType = userEntity.Entity.UserType
        };
    }

    public async Task<UserReturnDto?> UpdateInfo(string id, UpdateInfoDto updateInfoDto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null)
            return null;

        if (updateInfoDto.FullName is not null)
            user.FullName = updateInfoDto.FullName;
        if (updateInfoDto.Address is not null)
            user.Address = updateInfoDto.Address;
        if (updateInfoDto.PhoneNumber is not null)
            user.PhoneNumber = updateInfoDto.PhoneNumber;
        if (updateInfoDto.Status is not null)
            user.Status = updateInfoDto.Status.Value;

        await _dbContext.SaveChangesAsync();
        return new UserReturnDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            UserType = user.UserType
        };
    }

    public async Task<HelperReturnDto?> UpdateHelperInfo(string id, UpdateHelperDto updateHelperDto)
    {
        var helper = await _dbContext.Helpers.FirstOrDefaultAsync(x => x.Id == id);

        if (helper is null)
            return null;

        if (updateHelperDto.ExperienceDescription is not null)
            helper.ExperienceDescription = updateHelperDto.ExperienceDescription;
        if (updateHelperDto.ServicesOffered is not null)
            helper.ServicesOffered = updateHelperDto.ServicesOffered;
        if (updateHelperDto.ProposedPrice is not null)
            helper.ProposedPrice = updateHelperDto.ProposedPrice;

        await _dbContext.SaveChangesAsync();

        return new HelperReturnDto
        {
            Id = helper.Id,
            ExperienceDescription = helper.ExperienceDescription,
            ServicesOffered = helper.ServicesOffered,
            ProposedPrice = helper.ProposedPrice,
            AverageRating = helper.AverageRating,
            CompletedJobs = helper.CompletedJobs,
            CancelledJobs = helper.CancelledJobs
        };
    }

    public async Task<UserReturnDto[]> GetAllUsers(UserType? userType, UserStatus? status = UserStatus.Active)
    {
        var users = await _dbContext.Users
            .Where(x => (userType == null || x.UserType == userType) && x.Status == status)
            .ToArrayAsync();
        
        return users.Select(x => new UserReturnDto
        {
            Id = x.Id,
            FullName = x.FullName,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber,
            Address = x.Address,
            UserType = x.UserType
        }).ToArray();
    }
}