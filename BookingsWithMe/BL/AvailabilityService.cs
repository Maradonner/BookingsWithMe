using AutoMapper;
using BookingsWithMe.BL.Interfaces;
using BookingsWithMe.DAL.Entities;
using BookingsWithMe.DAL.Interfaces;
using BookingsWithMe.Models.Availability;

namespace BookingsWithMe.BL;

public class AvailabilityService : IAvailabilityService
{
    private readonly IAvailabilitiesRepository _availabilitiesDal;
    private readonly IMapper _mapper;

    public AvailabilityService(IAvailabilitiesRepository availabilitiesDal, IMapper mapper)
    {
        _availabilitiesDal = availabilitiesDal;
        _mapper = mapper;
    }

    public async Task<List<AvailabilityForDisplayDto>> GetAvailabilities(Guid userId)
    {
        var availabilities = await _availabilitiesDal.GetAvailabilities(userId);
        return _mapper.Map<List<AvailabilityForDisplayDto>>(availabilities);
    }

    public async Task<AvailabilityForDisplayDto> UpdateAvailability(AvailabilityForUpdateDto availabilityForUpdateDto)
    {
        var availability = _mapper.Map<Availability>(availabilityForUpdateDto);
        var updatedAvailability = await _availabilitiesDal.UpdateAvailability(availability);
        return _mapper.Map<AvailabilityForDisplayDto>(updatedAvailability);
    }
}