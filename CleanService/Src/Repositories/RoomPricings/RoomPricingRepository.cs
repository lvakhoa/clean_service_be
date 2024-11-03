using CleanService.Src.Models;

namespace CleanService.Src.Repositories.RoomPricings;

public class RoomPricingRepository : Repository<RoomPricing>, IRoomPricingRepository
{
    private readonly CleanServiceContext _dbContext;
    
    public RoomPricingRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}