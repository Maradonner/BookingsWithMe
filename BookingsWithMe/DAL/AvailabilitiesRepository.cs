using BookingsWithMe.DAL.Interfaces;
using BookingsWithMe.Data;
using BookingsWithMe.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
        return await _context.Availabilities
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    public async Task<Availability> UpdateAvailability(Availability availability)
    {
        _context.Availabilities.Update(availability);
        await _context.SaveChangesAsync();
        return availability;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken ct)
    {
        return await _context.SaveChangesAsync(ct) > 0;
    }
}
