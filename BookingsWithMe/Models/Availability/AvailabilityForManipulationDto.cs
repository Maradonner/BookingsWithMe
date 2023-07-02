namespace BookingsWithMe.Models.Availability;

public abstract class AvailabilityForManipulationDto
{
    public DayOfWeek Day { get; set; }
    public string Start { get; set; } = string.Empty;
    public string End { get; set; } = string.Empty;
    public Guid UserId { get; set; }
}