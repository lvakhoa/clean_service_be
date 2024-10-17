using CleanService.Src.Models;

namespace CleanService.Src.Modules.Auth.Repositories;

public interface IAuthRepository
{
    public Task<Users> GetUserByEmail(string email);
}