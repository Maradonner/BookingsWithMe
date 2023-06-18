using BookingsWithMe.BL.Interfaces;
using BookingsWithMe.Models.Availabilitie;
using Microsoft.AspNetCore.Mvc;

namespace BookingsWithMe.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AvailabilitiesController : ControllerBase
{
    private readonly IAvailabilityService _availabilityService;

    public AvailabilitiesController(IAvailabilityService availabilityService)
    {
        _availabilityService = availabilityService;
    }

    [HttpGet("{userId:Guid}")]
    public async Task<ActionResult<IEnumerable<AvailabilityForDisplayDto>>> GetAvailabilities(Guid userId)
    {
        var availabilitiesForDisplay = await _availabilityService.GetAvailabilities(userId);
        return availabilitiesForDisplay;
    }

    [HttpPut]
    public async Task<ActionResult<AvailabilityForDisplayDto>> PutAvailability(
        AvailabilityForUpdateDto availabilityForUpdateDto)
    {
        var availabilityForDisplayDto = await _availabilityService.UpdateAvailability(availabilityForUpdateDto);
        return availabilityForDisplayDto;
    }
}