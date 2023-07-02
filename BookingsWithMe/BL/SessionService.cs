using System.Collections.Concurrent;
using System.Text.Json;
using BookingsWithMe.BL.Interfaces;
using BookingsWithMe.Constants;
using BookingsWithMe.Cookies;
using BookingsWithMe.DAL.Entities;
using BookingsWithMe.DAL.Interfaces;

namespace BookingsWithMe.BL;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IWebCookie _webCookie;
    private readonly ILogger<SessionService> _logger;

    private ConcurrentDictionary<string, object> _sessionData = new();
    private Session? _sessionModel;

    public SessionService(ISessionRepository sessionDal, IWebCookie webCookie, ILogger<SessionService> logger)
    {
        _sessionRepository = sessionDal;
        _webCookie = webCookie;
        _logger = logger;
    }

    public async Task<Session> GetSessionAsync()
    {
        if (_sessionModel != null)
            return _sessionModel;

        Guid sessionId;
        var sessionString = _webCookie.Get(AuthConstants.SessionCookieName);
        sessionId = sessionString != null ? Guid.Parse(sessionString) : Guid.NewGuid();

        var data = await _sessionRepository.Get(sessionId);
        if (data == null)
        {
            data = await CreateSession();
            CreateSessionCookie(data.Id);
        }

        _sessionModel = data;
        if (data.SessionData != null)
            _sessionData = JsonSerializer.Deserialize<ConcurrentDictionary<string, object>>(data.SessionData) ??
                          new ConcurrentDictionary<string, object>();

        await _sessionRepository.Extend(data.Id);
        return data;
    }

    public async Task UpdateSessionDataAsync()
    {
        if (_sessionModel != null)
            await _sessionRepository.Update(_sessionModel.Id, JsonSerializer.Serialize(_sessionData));
        else
            throw new Exception("Session is not uploaded");
    }

    public void ResetSessionCache()
    {
        _sessionModel = null;
    }

    private void CreateSessionCookie(Guid sessionid)
    {
        _webCookie.Delete(AuthConstants.SessionCookieName);
        _webCookie.AddSecure(AuthConstants.SessionCookieName, sessionid.ToString());
    }

    public void AddValue(string key, object value)
    {
        if (_sessionData.ContainsKey(key))
            _sessionData[key] = value;
        else
            _sessionData.AddOrUpdate(key, value, (k, v) => value);
    }

    public void RemoveValue(string key)
    {
        _sessionData.TryRemove(key, out _);
    }

    private async Task<Session> CreateSession()
    {
        var data = new Session
        {
            Id = Guid.NewGuid(),
            Created = DateTime.Now,
            LastAccessed = DateTime.Now
        };
        await _sessionRepository.Create(data);
        return data;
    }
}