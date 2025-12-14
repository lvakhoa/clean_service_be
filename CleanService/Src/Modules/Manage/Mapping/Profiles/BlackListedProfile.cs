using AutoMapper;
using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
using CleanService.Src.Modules.Manage.Mapping.DTOs.BlackListed;

namespace CleanService.Src.Modules.Manage.Mapping.Profiles;

public class BlackListedProfile : Profile
{
    public BlackListedProfile()
    {
        CreateMap<CreateBlackListedDto, BlacklistedUsers>()
            .ConstructUsing(dto => new BlacklistedUsers
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                Reason = dto.Reason,
                BlacklistedBy = dto.BlacklistedBy,
                IsPermanent = dto.IsPermanent,
                ExpiryDate = dto.ExpiryDate,
            });
    }
}