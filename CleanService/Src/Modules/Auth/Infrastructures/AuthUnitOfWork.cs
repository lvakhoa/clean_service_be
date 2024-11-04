using CleanService.Src.Models;
using CleanService.Src.Repositories.Helper;
using CleanService.Src.Repositories.User;

namespace CleanService.Src.Modules.Auth.Infrastructures;

public class AuthUnitOfWork : IAuthUnitOfWork
{
    private readonly CleanServiceContext _dbContext;
    
    public IUserRepository UserRepository { get; }
    
    public IHelperRepository HelperRepository { get; }
    
    public AuthUnitOfWork(CleanServiceContext dbContext, IUserRepository userRepository, IHelperRepository helperRepository)
    {
        _dbContext = dbContext;
        UserRepository = userRepository;
        HelperRepository = helperRepository;
    }
    
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}