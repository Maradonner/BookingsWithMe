using BookingsWithMe.Entities;

namespace BookingsWithMe.Models.User;

public abstract class UserForManipulationDto
{
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
}
