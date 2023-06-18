using BookingsWithMe.Models.User;
using BookingsWithMe.ResourceParameters;

namespace BookingsWithMe.BL.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserForDisplayDto>> GetUsersAsync(UserResourceParameters userResourceParameters,
        CancellationToken ct);

    Task<UserForDisplayDto> GetUserAsync(Guid id, CancellationToken ct);
    Task<UserForDisplayDto> CreateUserAsync(UserForCreationDto userForCreationDto, CancellationToken ct);
    Task<UserForDisplayDto> UpdateUserAsync(UserForUpdateDto userForUpdateDto, CancellationToken ct);
    Task<bool> DeleteUserAsync(Guid id, CancellationToken ct);
    Task<bool> EmailExistsAsync(string email, CancellationToken ct);
}