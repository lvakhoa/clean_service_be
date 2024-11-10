using CleanService.Src.Models;

namespace CleanService.Src.Repositories.HelperAvailabilities;

public class HelperAvailabilityRepository : Repository<HelperAvailability, PartialHelperAvailability>, IHelperAvailabilityRepository
{
    private readonly CleanServiceContext _dbContext;

    public HelperAvailabilityRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}