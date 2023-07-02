using BookingsWithMe.DAL.Entities;
using BookingsWithMe.Models.User;
using BookingsWithMe.ResourceParameters;

namespace BookingsWithMe.BL.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync(UserResourceParameters userResourceParameters,
        CancellationToken ct);

    Task<User?> GetUserAsync(Guid id, CancellationToken ct);
    Task<User> CreateUserAsync(User user, CancellationToken ct);
    Task<User?> UpdateUserAsync(User user, CancellationToken ct);
    Task<bool> DeleteUserAsync(User user, CancellationToken ct);
    Task<bool> EmailExistsAsync(string email, CancellationToken ct);
}