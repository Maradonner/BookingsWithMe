using AutoMapper;
using BookingsWithMe.BL.Interfaces;
using BookingsWithMe.DAL.Interfaces;
using BookingsWithMe.Entities;
using BookingsWithMe.Models.Availabilitie;

namespace BookingsWithMe.BL;

public class AvailabilityService : IAvailabilityService
{
    private readonly IAvailabilitiesRepository _availabilitiesDAL;
    private readonly IMapper _mapper;
    public AvailabilityService(IAvailabilitiesRepository availabilitiesDAL, IMapper mapper)
    {
        _availabilitiesDAL = availabilitiesDAL;
        _mapper = mapper;
    }

    public async Task<List<AvailabilityForDisplayDto>> GetAvailabilities(Guid userId)
    {
        var availabilities = await _availabilitiesDAL.GetAvailabilities(userId);
        return _mapper.Map<List<AvailabilityForDisplayDto>>(availabilities);
    }

    public async Task<AvailabilityForDisplayDto> UpdateAvailability(AvailabilityForUpdateDto availabilityForUpdateDto)
    {
        var availability = _mapper.Map<Availability>(availabilityForUpdateDto);
        var updatedAvailability = await _availabilitiesDAL.UpdateAvailability(availability);
        return _mapper.Map<AvailabilityForDisplayDto>(updatedAvailability);
    }
}