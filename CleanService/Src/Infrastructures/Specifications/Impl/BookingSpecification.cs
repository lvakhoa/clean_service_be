using CleanService.Src.Models;
using CleanService.Src.Models.Domains;
using CleanService.Src.Models.Enums;

namespace CleanService.Src.Infrastructures.Specifications.Impl;

public class BookingSpecification
{
    public static BaseSpecification<Bookings> GetBookingByIdSpec(Guid id)
    {
        return new BaseSpecification<Bookings>(x => x.Id == id);
    }

    public static BaseSpecification<Bookings> GetBookingByOrderIdSpec(int orderId)
    {
        return new BaseSpecification<Bookings>(x => x.OrderId == orderId);
    }

    public static BaseSpecification<Bookings> GetBookingByUserIdAndStatusSpec(string userId, UserType userType, IEnumerable<BookingStatus> statuses)
    {
        return new BaseSpecification<Bookings>(x =>
            (userType == UserType.Helper && x.HelperId == userId) ||
            (userType == UserType.Customer && x.CustomerId == userId) &&
            (!statuses.Any() || statuses.Contains(x.Status)));
    }

    public static BaseSpecification<Bookings> GetBookingByUserIdSpec(string? userId, UserType userType)
    {
        return new BaseSpecification<Bookings>(userId != null ? x =>
            (userType == UserType.Helper && x.HelperId == userId) ||
            (userType == UserType.Customer && x.CustomerId == userId) : null);
    }

    public static BaseSpecification<Bookings> GetPagedBookingsSpec(int? page, int? limit)
    {
        var spec = new BaseSpecification<Bookings>(null);
        if (page.HasValue && limit.HasValue)
        {
            spec.ApplyPaging((page.Value - 1) * limit.Value, limit.Value);
        }
        return spec;
    }
}
