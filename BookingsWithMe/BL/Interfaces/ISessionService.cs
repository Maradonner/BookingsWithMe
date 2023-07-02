using BookingsWithMe.DAL.Entities;

namespace BookingsWithMe.BL.Interfaces;

public interface ISessionService
{
    Task<Session> GetSessionAsync();

    Task UpdateSessionDataAsync();

    void ResetSessionCache();

    void AddValue(string key, object value);

    void RemoveValue(string key);
}