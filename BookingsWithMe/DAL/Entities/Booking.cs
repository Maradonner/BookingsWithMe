using System.ComponentModel.DataAnnotations.Schema;

namespace BookingsWithMe.DAL.Entities;

public class Booking
{
    public Guid Id { get; set; }
    public DateTimeOffset BookingTime { get; set; }
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerTimeZone { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; }
    public string Status { get; set; } = string.Empty; //"Scheduled", "Cancelled", "Completed"


    //Who created this time slot
    [ForeignKey(nameof(Creator))]
    public Guid CreatorId { get; set; }
    public User? Creator { get; set; }


    //Who booked this time slot
    [ForeignKey(nameof(User))]
    public Guid? UserId { get; set; }
    public User? User { get; set; }



    [ForeignKey(nameof(Availability))]
    public Guid AvailabilityId { get; set; }
    public Availability? Availability { get; set; }
}