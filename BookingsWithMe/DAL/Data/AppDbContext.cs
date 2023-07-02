using BookingsWithMe.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingsWithMe.DAL.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Availability> Availabilities { get; set; }
    public DbSet<BlockedTime> BlockedTimes { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Session> Sessions { get; set; }
}