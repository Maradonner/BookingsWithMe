using System.ComponentModel.DataAnnotations.Schema;

namespace BookingsWithMe.Models.Booking;

public class BookingForCreationDto
{
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public Guid AvailabilityId { get; set; }
}
