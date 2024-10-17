using CleanService.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanService.Src.Modules.Auth.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly CleanServiceContext _dbContext;
    
    public AuthRepository(CleanServiceContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Users> GetUserByEmail(string email)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        if(user == null)
            throw new KeyNotFoundException("User not found");
        
        return user;
    }
}