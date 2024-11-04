using CleanService.Src.Models;

namespace CleanService.Src.Repositories.Helper;

public class HelperRepository : Repository<Helpers, PartialHelper>, IHelperRepository
{
    private readonly CleanServiceContext _dbContext;
    
    public HelperRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }    
}