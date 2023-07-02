using BookingsWithMe.DAL.Data;
using BookingsWithMe.DAL.Entities;
using BookingsWithMe.DAL.Interfaces;
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

    public async Task<List<User>> GetUserListAsync(UserResourceParameters userResourceParameters, CancellationToken ct)
    {
        var collection = GetUserAsQueryable().AsNoTracking();

        var pagedList = await PagedList<User>.CreateAsync(collection, userResourceParameters.PageNumber,
            userResourceParameters.PageSize, ct);

        return pagedList;
    }

    public IQueryable<User> GetUserAsQueryable()
    {
        return _context.Users.AsQueryable();
    }

    public async Task<User?> GetUserAsync(Guid id, CancellationToken ct)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task CreateUserAsync(User user, CancellationToken ct)
    {
        await _context.Users.AddAsync(user, ct);
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
    }

    public void DeleteUser(User user)
    {
        _context.Users.Remove(user);
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