using AutoMapper;
using BookingsWithMe.BL.Interfaces;
using BookingsWithMe.DAL.Entities;
using BookingsWithMe.DAL.Interfaces;
using BookingsWithMe.Models.User;
using BookingsWithMe.ResourceParameters;

namespace BookingsWithMe.BL;

public class UserService : IUserService
{
    private readonly IUsersRepository _usersRepository;

    public UserService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository ??
                           throw new ArgumentNullException(nameof(usersRepository));
    }

    public async Task<IEnumerable<User>> GetUsersAsync(UserResourceParameters userResourceParameters,
        CancellationToken ct)
    {
        var users = await _usersRepository.GetUserListAsync(userResourceParameters, ct);
        return users;
    }

    public async Task<User?> GetUserAsync(Guid id, CancellationToken ct)
    {
        var user = await _usersRepository.GetUserAsync(id, ct);
        return user;
    }

    public async Task<User> CreateUserAsync(User user, CancellationToken ct)
    {
        await _usersRepository.CreateUserAsync(user, ct);
        await _usersRepository.SaveChangesAsync(ct);
        return user;
    }

    public async Task<User?> UpdateUserAsync(User user, CancellationToken ct)
    {
        var existingUser = await _usersRepository.GetUserAsync(user.Id, ct);
        if (existingUser == null)
            return null;

        _usersRepository.UpdateUser(user);
        await _usersRepository.SaveChangesAsync(ct);
        return user;
    }

    public async Task<bool> DeleteUserAsync(User user, CancellationToken ct)
    {
        _usersRepository.DeleteUser(user);
        return await _usersRepository.SaveChangesAsync(ct);
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken ct)
    {
        return await _usersRepository.EmailExistsAsync(email, ct);
    }
}