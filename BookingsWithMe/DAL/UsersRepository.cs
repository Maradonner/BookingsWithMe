using BookingsWithMe.DAL.Interfaces;
using BookingsWithMe.Data;
using BookingsWithMe.Entities;
using BookingsWithMe.Helpers;
using BookingsWithMe.ResourceParameters;
using Microsoft.EntityFrameworkCore;

namespace BookingsWithMe.DAL;

public class UsersRepository : IUsersRepository
{
    private readonly AppDbContext _context;

    public UsersRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<User>> GetUsersAsync(UserResourceParameters userResourceParameters, CancellationToken ct)
    {
        var collection = _context.Users as IQueryable<User>;

        var pagedList = await PagedList<User>.CreateAsync(collection, userResourceParameters.PageNumber,
            userResourceParameters.PageSize, ct);

        return pagedList;
    }

    public async Task<User> GetUserAsync(Guid id, CancellationToken ct)
    {
        return await _context.Users.FindAsync(new object[] { id }, ct);
    }

    public async Task<User> CreateUserAsync(User user, CancellationToken ct)
    {
        var result = await _context.Users.AddAsync(user, ct);
        await _context.SaveChangesAsync(ct);
        return result.Entity;
    }

    public async Task<User> UpdateUserAsync(User user, CancellationToken ct)
    {
        var result = _context.Users.Update(user);
        await _context.SaveChangesAsync(ct);
        return result.Entity;
    }

    public async Task<bool> DeleteUserAsync(Guid id, CancellationToken ct)
    {
        var user = await _context.Users.FindAsync(new object[] { id }, ct);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken ct)
    {
        return await _context.SaveChangesAsync(ct) > 0;
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken ct)
    {
        return await _context.Users.AnyAsync(x => x.Email == email, ct);
    }
}