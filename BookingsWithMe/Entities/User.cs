namespace BookingsWithMe.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public byte[] Photo { get; set; } = new byte[64];
    public List<Availability> Availabilities { get; set; } = new();
    public List<BlockedTime> BlockedTimes { get; set; } = new();
}