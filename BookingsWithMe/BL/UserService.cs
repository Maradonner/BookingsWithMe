using AutoMapper;
using BookingsWithMe.BL.Interfaces;
using BookingsWithMe.DAL.Interfaces;
using BookingsWithMe.Entities;
using BookingsWithMe.Models.User;
using BookingsWithMe.ResourceParameters;

namespace BookingsWithMe.BL;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUsersRepository _usersRepository;

    public UserService(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository ??
                           throw new ArgumentNullException(nameof(usersRepository));
        _mapper = mapper ??
                  throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<UserForDisplayDto>> GetUsersAsync(UserResourceParameters userResourceParameters,
        CancellationToken ct)
    {
        var users = await _usersRepository.GetUsersAsync(userResourceParameters, ct);
        return _mapper.Map<IEnumerable<UserForDisplayDto>>(users);
    }

    public async Task<UserForDisplayDto> GetUserAsync(Guid id, CancellationToken ct)
    {
        var user = await _usersRepository.GetUserAsync(id, ct);
        return _mapper.Map<UserForDisplayDto>(user);
    }

    public async Task<UserForDisplayDto> CreateUserAsync(UserForCreationDto userForCreationDto, CancellationToken ct)
    {
        var user = _mapper.Map<User>(userForCreationDto);
        var createdUser = await _usersRepository.CreateUserAsync(user, ct);
        return _mapper.Map<UserForDisplayDto>(createdUser);
    }

    public async Task<UserForDisplayDto> UpdateUserAsync(UserForUpdateDto userForUpdateDto, CancellationToken ct)
    {
        var user = _mapper.Map<User>(userForUpdateDto);
        var updatedUser = await _usersRepository.UpdateUserAsync(user, ct);
        return _mapper.Map<UserForDisplayDto>(updatedUser);
    }

    public async Task<bool> DeleteUserAsync(Guid id, CancellationToken ct)
    {
        return await _usersRepository.DeleteUserAsync(id, ct);
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken ct)
    {
        return await _usersRepository.EmailExistsAsync(email, ct);
    }
}