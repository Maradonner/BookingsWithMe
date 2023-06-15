namespace BookingsWithMe.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public byte[] Photo { get; set; } = new byte[64];
}