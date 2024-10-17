using CleanService.Src.Models;
using CleanService.Src.Modules.Auth.Repositories;

namespace CleanService.Src.Modules.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    
    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
    
    public Task RegisterUser(Users user)
    {
        throw new NotImplementedException();
    }
}