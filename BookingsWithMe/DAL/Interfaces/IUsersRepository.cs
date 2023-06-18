using BookingsWithMe.Entities;
using BookingsWithMe.ResourceParameters;

namespace BookingsWithMe.DAL.Interfaces;

public interface IUsersRepository
{
    Task<List<User>> GetUsersAsync(UserResourceParameters userResourceParameters, CancellationToken ct);
    Task<User> GetUserAsync(Guid id, CancellationToken ct);
    Task<User> CreateUserAsync(User user, CancellationToken ct);
    Task<User> UpdateUserAsync(User user, CancellationToken ct);
    Task<bool> DeleteUserAsync(Guid id, CancellationToken ct);
    Task<bool> EmailExistsAsync(string email, CancellationToken ct);
    Task<bool> SaveChangesAsync(CancellationToken ct);
}
