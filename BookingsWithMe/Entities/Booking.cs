namespace BookingsWithMe.Entities;

public class Booking
{
    public Guid Id { get; set; }
    public DateTime BookingTime { get; set; }
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerTimeZone { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public string Status { get; set; }  //"Scheduled", "Cancelled", "Completed"
    public Guid UserId { get; set; }
    public User User { get; set; }
}
