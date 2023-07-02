using System.ComponentModel.DataAnnotations.Schema;

namespace BookingsWithMe.DAL.Entities;

public class Availability
{
    public Guid Id { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeSpan Start { get; set; }
    public TimeSpan End { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public User User { get; set; } = new();
    public List<Booking> Bookings { get; set; } = new();
}