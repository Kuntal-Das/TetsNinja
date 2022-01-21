using System.Linq;

namespace TestNinja.Mocking;

public interface IBookingRepo
{
    IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
}

public class BookingRepo : IBookingRepo
{
    public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
    {
        var unitOfWork = new UnitOfWork();
        var bookings = unitOfWork
            .Query<Booking>()
            .Where(b => b.Status != "Cancelled");

        if (excludedBookingId.HasValue)
            return bookings.Where(b => b.Id != excludedBookingId.Value);
        
        return bookings;
    }
}