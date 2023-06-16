using System.ComponentModel.DataAnnotations.Schema;

namespace BookingsWithMe.Models.Availabilitie;

public class AvailabilityForManipulationDto
{
    public DayOfWeek Day { get; set; }
    public string Start { get; set; }
    public string End { get; set; }
    public Guid UserId { get; set; }
}
