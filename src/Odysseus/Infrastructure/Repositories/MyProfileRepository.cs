using Microsoft.EntityFrameworkCore;
using Odysseus.Host.Application.Interfaces;
using Odysseus.Host.Data;
using Odysseus.Host.Domain.Entities;

namespace Odysseus.Host.Infrastructure.Repositories;

/// <summary>
/// Entity Framework implementation of IMyProfileRepository
/// </summary>
public class MyProfileRepository : IMyProfileRepository
{
    private readonly ApplicationDbContext _context;

    public MyProfileRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc />
    public async Task<MyProfile?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _context.MyProfiles
            .Include(p => p.JobPreferences)
            .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<MyProfile?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.MyProfiles
            .Include(p => p.JobPreferences)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<MyProfile?> GetByIdAndUserIdAsync(int id, string userId, CancellationToken cancellationToken = default)
    {
        return await _context.MyProfiles
            .Include(p => p.JobPreferences)
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<MyProfile> AddAsync(MyProfile profile, CancellationToken cancellationToken = default)
    {
        if (profile == null)
            throw new ArgumentNullException(nameof(profile));

        profile.CreatedAt = DateTime.UtcNow;
        profile.UpdatedAt = DateTime.UtcNow;

        _context.MyProfiles.Add(profile);
        await _context.SaveChangesAsync(cancellationToken);

        return profile;
    }

    /// <inheritdoc />
    public async Task<MyProfile> UpdateAsync(MyProfile profile, CancellationToken cancellationToken = default)
    {
        if (profile == null)
            throw new ArgumentNullException(nameof(profile));

        var existingProfile = await _context.MyProfiles
            .FirstOrDefaultAsync(p => p.Id == profile.Id, cancellationToken);

        if (existingProfile == null)
            throw new InvalidOperationException($"Profile with ID {profile.Id} not found");

        // Update properties
        existingProfile.Passport = profile.Passport;
        existingProfile.NeedRelocationSupport = profile.NeedRelocationSupport;
        existingProfile.NeedSponsorship = profile.NeedSponsorship;
        existingProfile.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
        return existingProfile;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id, string userId, CancellationToken cancellationToken = default)
    {
        var profile = await _context.MyProfiles
            .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId, cancellationToken);

        if (profile == null)
            return false;

        _context.MyProfiles.Remove(profile);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsForUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _context.MyProfiles
            .AnyAsync(p => p.UserId == userId, cancellationToken);
    }
}
