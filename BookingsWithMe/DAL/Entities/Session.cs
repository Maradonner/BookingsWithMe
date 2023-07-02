using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingsWithMe.DAL.Entities;

public class Session
{
    public Guid Id { get; set; }

    [ConcurrencyCheck]
    public string? SessionData { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastAccessed { get; set; }

    [ForeignKey(nameof(User))]
    public Guid? UserId { get; set; }
    public User? User { get; set; }
}