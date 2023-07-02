using BookingsWithMe.DAL.Entities;
using BookingsWithMe.Models.Availability;

namespace BookingsWithMe.Models.User;

public class UserForDisplayDto : UserForManipulationDto
{
    public Guid Id { get; set; }
    public List<AvailabilityForDisplayDto> Availabilities { get; set; } = new();
    public List<BlockedTime> BlockedTimes { get; set; } = new();
    public List<Booking> Bookings { get; set; } = new();
}