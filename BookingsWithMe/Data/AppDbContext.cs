using BookingsWithMe.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingsWithMe.Data;

public class AppDbContext : DbContext
{
    public DbSet<Availability> Availabilities { get; set; }
    public DbSet<BlockedTime> BlockedTimes { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<User> Users { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}