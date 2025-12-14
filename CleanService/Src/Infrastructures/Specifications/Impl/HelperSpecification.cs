using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
using CleanService.Src.Models.Enums;

namespace CleanService.Src.Infrastructures.Specifications.Impl;

public class HelperSpecification
{
    public static BaseSpecification<Helpers> GetHelperByIdSpec(string id)
    {
        return new BaseSpecification<Helpers>(x => x.Id == id);
    }

    public static BaseSpecification<Helpers> GetHelperByStatusSpec(UserStatus? userStatus = UserStatus.Active)
    {
        return new BaseSpecification<Helpers>(x => x.User.Status == userStatus);
    }
}
