using BookingsWithMe.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingsWithMe.Extensions;

public static class MigrationExtension
{
    public static void ApplyMigrations(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        dbContext.Database.Migrate();
    }
}