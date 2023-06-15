using System.ComponentModel.DataAnnotations.Schema;

namespace BookingsWithMe.Entities;

public class BlockedTime
{
    public Guid Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string? Reason { get; set; } //Holiday
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public User User { get; set; }
}

