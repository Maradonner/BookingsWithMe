using BookingsWithMe.DAL.Data;
using BookingsWithMe.DAL.Entities;
using BookingsWithMe.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingsWithMe.DAL;

public class AvailabilitiesRepository : IAvailabilitiesRepository
{
    private readonly AppDbContext _context;

    public AvailabilitiesRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<Availability>> GetAvailabilities(Guid userId)
    {
        var availabilities = await _context.Availabilities.ToListAsync();

        var availabilitiesForReturn = await _context.Availabilities
            .AsNoTracking()
            .Where(a => a.UserId == userId)
            .ToListAsync();

        return availabilitiesForReturn;
    }

    public void UpdateAvailability(Availability availability)
    {
        _context.Availabilities.Add(availability);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken ct)
    {
        return await _context.SaveChangesAsync(ct) > 0;
    }
}