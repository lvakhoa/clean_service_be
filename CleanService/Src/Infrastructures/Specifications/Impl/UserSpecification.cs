using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
using CleanService.Src.Models.Enums;

namespace CleanService.Src.Infrastructures.Specifications.Impl;

public class UserSpecification
{
    public static BaseSpecification<Users> GetUserByIdSpec(string id)
    {
        return new BaseSpecification<Users>(x => x.Id == id);
    }

    public static BaseSpecification<Users> GetUserByEmailSpec(string email)
    {
        return new BaseSpecification<Users>(x => x.Email == email);
    }

    public static BaseSpecification<Users> GetUserByPhoneNumberSpec(string phone)
    {
        return new BaseSpecification<Users>(x => x.PhoneNumber == phone);
    }

    public static BaseSpecification<Users> GetUserByStatusOrTypeSpec(UserType? userType, UserStatus? status)
    {
        return new BaseSpecification<Users>(x =>
            (!userType.HasValue || x.UserType == userType) && (!status.HasValue || x.Status == status));
    }
}
