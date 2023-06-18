using System.ComponentModel.DataAnnotations.Schema;

namespace BookingsWithMe.Entities;

public class Booking
{
    public Guid Id { get; set; }
    public DateTime BookingTime { get; set; }
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerTimeZone { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public string Status { get; set; } = string.Empty; //"Scheduled", "Cancelled", "Completed"

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; } //Who booked this time slot

    public virtual User User { get; set; }

    [ForeignKey(nameof(Availability))]
    public Guid AvailabilityId { get; set; }

    public virtual Availability Availability { get; set; }
}