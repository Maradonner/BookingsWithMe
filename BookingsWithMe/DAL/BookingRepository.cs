using BookingsWithMe.DAL.Data;
using BookingsWithMe.DAL.Entities;
using BookingsWithMe.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingsWithMe.DAL;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Booking?> GetBookingAsync(Guid id)
    {
        var bookings = await _context.Bookings
             .AsNoTracking()
             .Include(x => x.Creator)
             .Include(x => x.User)
             .Include(x => x.Availability)
             .FirstOrDefaultAsync(x => x.Id == id);

        return bookings;
    }

    public async Task<List<Booking>> GetBookingListAsync()
    {
        var bookings = await _context.Bookings
            .AsNoTracking()
            .Include(x => x.Creator)
            .Include(x => x.User)
            .Include(x => x.Availability)
            .ToListAsync();

        return bookings;
    }

    public async Task AddBookingAsync(Booking booking)
    {
        await _context.Bookings.AddAsync(booking);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken ct)
    {
        return await _context.SaveChangesAsync(ct) > 0;
    }

    public void UpdateBooking(Booking booking)
    {
        _context.Bookings.Update(booking);
    }
}
