using AutoMapper;
using BookingsWithMe.BL.Interfaces;
using BookingsWithMe.DAL.Entities;
using BookingsWithMe.DAL.Interfaces;
using BookingsWithMe.Models.Availability;

namespace BookingsWithMe.BL;

public class AvailabilityService : IAvailabilityService
{
    private readonly IAvailabilitiesRepository _availabilitiesRepository;
    private readonly IMapper _mapper;

    public AvailabilityService(IAvailabilitiesRepository availabilitiesRepository, IMapper mapper)
    {
        _availabilitiesRepository = availabilitiesRepository;
        _mapper = mapper;
    }

    public async Task<List<AvailabilityForDisplayDto>> GetAvailabilities(Guid userId)
    {
        var availabilities = await _availabilitiesRepository.GetAvailabilities(userId);
        return _mapper.Map<List<AvailabilityForDisplayDto>>(availabilities);
    }

    public async Task<AvailabilityForDisplayDto> UpdateAvailability(AvailabilityForUpdateDto availabilityForUpdateDto)
    {
        var availability = _mapper.Map<Availability>(availabilityForUpdateDto);

        _availabilitiesRepository.UpdateAvailability(availability);
        await _availabilitiesRepository.SaveChangesAsync(default);

        return _mapper.Map<AvailabilityForDisplayDto>(availability);
    }
}