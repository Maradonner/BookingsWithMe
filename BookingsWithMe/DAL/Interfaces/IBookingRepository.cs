using BookingsWithMe.DAL.Entities;

namespace BookingsWithMe.DAL.Interfaces;

public interface IBookingRepository
{
    Task<List<Booking>> GetBookingListAsync();
    Task<Booking?> GetBookingAsync(Guid id);
    Task AddBookingAsync(Booking booking);
    void UpdateBooking(Booking booking);
    Task<bool> SaveChangesAsync(CancellationToken ct);
}
