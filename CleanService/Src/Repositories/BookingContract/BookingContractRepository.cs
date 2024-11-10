using CleanService.Src.Models;

namespace CleanService.Src.Repositories.BookingContract;

public class BookingContractRepository : Repository<BookingContracts, PartialBookingContracts>, IBookingContractRepository
{
    private readonly CleanServiceContext _dbContext;

    public BookingContractRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}