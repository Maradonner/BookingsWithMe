using BookingsWithMe.DAL.Data;
using BookingsWithMe.DAL.Entities;
using BookingsWithMe.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingsWithMe.DAL;

public class SessionRepository : ISessionRepository
{
    private readonly AppDbContext _context;

    public SessionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Create(Session model)
    {
        _context.Sessions.Add(model);
        await _context.SaveChangesAsync();
    }

    public async Task<Session?> Get(Guid sessionId)
    {
        return await _context.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId);
    }

    public async Task Update(Guid dbSessionId, string sessionData)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Id == dbSessionId);
        if (session != null)
        {
            session.SessionData = sessionData;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // A concurrency violation occurred. Handle it accordingly.
                // For example, you could refresh the entity and retry the update, or notify the user about the conflict.
            }
        }
    }

    public async Task Extend(Guid dbSessionId)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Id == dbSessionId);
        if (session != null)
        {
            session.LastAccessed = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}