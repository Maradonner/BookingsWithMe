using System.ComponentModel.DataAnnotations.Schema;

namespace BookingsWithMe.Entities;

public class Availability
{
    public int Id { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeSpan Start { get; set; }
    public TimeSpan End { get; set; }
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public User User { get; set; }
}
