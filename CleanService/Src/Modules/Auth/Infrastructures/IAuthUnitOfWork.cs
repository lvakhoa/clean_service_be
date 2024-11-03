using CleanService.Src.Repositories.Helper;
using CleanService.Src.Repositories.User;

namespace CleanService.Src.Modules.Auth.Infrastructures;

public interface IAuthUnitOfWork
{
    // IUserRepository UserRepository { get; }
    //
    // IHelperRepository HelperRepository { get; }
    
    void SaveChangesAsync();
}