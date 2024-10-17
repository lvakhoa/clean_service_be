using CleanService.Src.Models;

namespace CleanService.Src.Modules.Auth.Services;

public interface IAuthService
{
    public Task RegisterUser(Users user);
}