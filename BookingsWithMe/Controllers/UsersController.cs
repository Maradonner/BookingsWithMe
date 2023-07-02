using AutoMapper;
using BookingsWithMe.BL.Interfaces;
using BookingsWithMe.DAL.Entities;
using BookingsWithMe.Models.User;
using BookingsWithMe.ResourceParameters;
using Microsoft.AspNetCore.Mvc;

namespace BookingsWithMe.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserForDisplayDto>>> GetUsers(
        [FromQuery] UserResourceParameters usersResourceParameters, CancellationToken ct)
    {
        var usersForDisplay = await _userService.GetUsersAsync(usersResourceParameters, ct);
        return Ok(usersForDisplay);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserForDisplayDto>> GetUser(Guid id, CancellationToken ct)
    {
        var user = await _userService.GetUserAsync(id, ct);

        var userForReturn = _mapper.Map<UserForDisplayDto>(user);

        return userForReturn;
    }

    [HttpPost]
    public async Task<ActionResult<UserForDisplayDto>> PostUser(UserForCreationDto userForCreationDto,
        CancellationToken ct)
    {
        if (await _userService.EmailExistsAsync(userForCreationDto.Email, ct))
            return BadRequest("User with that email is already exists");

        var user = _mapper.Map<User>(userForCreationDto);

        var userForDisplay = await _userService.CreateUserAsync(user, ct);

        var userForReturn = _mapper.Map<UserForDisplayDto>(userForDisplay);

        return CreatedAtAction("GetUser", new { id = userForDisplay.Id }, userForReturn);
    }

    [HttpPut]
    public async Task<ActionResult<UserForDisplayDto>> PutUser(UserForUpdateDto userForUpdateDto, CancellationToken ct)
    {
        var user = _mapper.Map<User>(userForUpdateDto);

        var userForDisplayDto = await _userService.UpdateUserAsync(user, ct);

        if (userForDisplayDto == null)
            return BadRequest("Failed to update");

        var userForReturn = _mapper.Map<UserForDisplayDto>(userForDisplayDto);

        return userForReturn;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken ct)
    {
        var user = await _userService.GetUserAsync(id, ct);
        if(user == null)
            return NotFound();
        var result = await _userService.DeleteUserAsync(user, ct);
        if (!result)
            return BadRequest("Just do not deleted");

        return NoContent();
    }
}