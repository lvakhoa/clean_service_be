using CleanService.Src.Models;

namespace CleanService.Src.Repositories.BookingDetail;

public class BookingDetailRepository : Repository<BookingDetails, PartialBookingDetails>, IBookingDetailRepository
{
    private readonly CleanServiceContext _dbContext;
    
    public BookingDetailRepository(CleanServiceContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}