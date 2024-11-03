using CleanService.Src.Models;

namespace CleanService.Src.Repositories.DurationPrices;

public class DurationPriceRepository : Repository<DurationPrice>, IDurationPriceRepository
{
    private readonly CleanServiceContext _dbContext;
    
    public DurationPriceRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }   
}