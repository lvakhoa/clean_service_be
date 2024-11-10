using CleanService.Src.Models;

namespace CleanService.Src.Repositories.BlacklistedUser;

public class BlacklistedUserRepository : Repository<BlacklistedUsers, PartialBlacklistedUsers>, IBlacklistedUserRepository
{
    private readonly CleanServiceContext _dbContext;

    public BlacklistedUserRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}