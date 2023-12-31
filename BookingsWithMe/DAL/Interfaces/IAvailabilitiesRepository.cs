﻿using BookingsWithMe.DAL.Entities;

namespace BookingsWithMe.DAL.Interfaces;

public interface IAvailabilitiesRepository
{
    Task<List<Availability>> GetAvailabilities(Guid userId);
    Task<Availability> UpdateAvailability(Availability availability);
    Task<bool> SaveChangesAsync(CancellationToken ct);
}