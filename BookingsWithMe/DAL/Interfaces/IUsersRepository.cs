using BookingsWithMe.DAL.Entities;
using BookingsWithMe.ResourceParameters;

namespace BookingsWithMe.DAL.Interfaces;

public interface IUsersRepository
{
    Task<List<User>> GetUserListAsync(UserResourceParameters userResourceParameters, CancellationToken ct);
    Task<User?> GetUserAsync(Guid id, CancellationToken ct);
    Task CreateUserAsync(User user, CancellationToken ct);
    void UpdateUser(User user);
    void DeleteUser(User user);
    Task<bool> EmailExistsAsync(string email, CancellationToken ct);
    Task<bool> SaveChangesAsync(CancellationToken ct);
}