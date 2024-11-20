using CleanService.Src.Models;

namespace CleanService.Src.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class RoleExposeAttribute : Attribute
{
    public UserType[] Roles { get; }

    public RoleExposeAttribute(params UserType[] roles)
    {
        Roles = roles;
    }
}