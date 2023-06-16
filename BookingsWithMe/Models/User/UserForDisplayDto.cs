using BookingsWithMe.Entities;

namespace BookingsWithMe.Models.User;

public class UserForDisplayDto : UserForManipulationDto
{
    public Guid Id { get; set; }
    public List<Availability> Availabilities { get; set; } = new();
    public List<BlockedTime> BlockedTimes { get; set; } = new();
    public List<Booking> Bookings { get; set; } = new();
}
