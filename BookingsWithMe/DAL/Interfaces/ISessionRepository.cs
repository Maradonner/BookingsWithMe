using BookingsWithMe.DAL.Entities;

namespace BookingsWithMe.DAL.Interfaces;

public interface ISessionRepository
{
    Task<Session?> Get(Guid sessionId);

    Task Update(Guid dbSessionId, string sessionData);

    Task Extend(Guid dbSessionId);

    Task Create(Session model);
}