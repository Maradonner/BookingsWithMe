namespace BookingsWithMe.DAL.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;

    public List<Availability>? Availabilities { get; set; }
    public List<BlockedTime>? BlockedTimes { get; set; }
    public List<Booking>? Bookings { get; set; }
}