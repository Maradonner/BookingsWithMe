using BookingsWithMe.Models.Availability;

namespace BookingsWithMe.BL.Interfaces;

public interface IAvailabilityService
{
    Task<List<AvailabilityForDisplayDto>> GetAvailabilities(Guid userId);
    Task<AvailabilityForDisplayDto> UpdateAvailability(AvailabilityForUpdateDto availabilityForUpdateDto);
}