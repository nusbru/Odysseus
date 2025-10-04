using Microsoft.EntityFrameworkCore;
using Odysseus.Host.Application.Interfaces;
using Odysseus.Host.Data;
using Odysseus.Host.Domain.Entities;
using Odysseus.Host.Domain.Enums;

namespace Odysseus.Host.Infrastructure.Repositories;

/// <summary>
/// Entity Framework implementation of IMyJobPreferenceRepository
/// </summary>
public class MyJobPreferenceRepository : IMyJobPreferenceRepository
{
    private readonly ApplicationDbContext _context;

    public MyJobPreferenceRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<MyJobPreference>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _context.MyJobPreferences
            .Include(jp => jp.MyProfile)
            .Where(jp => jp.UserId == userId)
            .OrderBy(jp => jp.Title)
            .ThenByDescending(jp => jp.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<MyJobPreference>> GetByProfileIdAsync(int profileId, CancellationToken cancellationToken = default)
    {
        return await _context.MyJobPreferences
            .Include(jp => jp.MyProfile)
            .Where(jp => jp.MyProfileId == profileId)
            .OrderBy(jp => jp.Title)
            .ThenByDescending(jp => jp.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<MyJobPreference?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.MyJobPreferences
            .Include(jp => jp.MyProfile)
            .FirstOrDefaultAsync(jp => jp.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<MyJobPreference?> GetByIdAndUserIdAsync(int id, string userId, CancellationToken cancellationToken = default)
    {
        return await _context.MyJobPreferences
            .Include(jp => jp.MyProfile)
            .FirstOrDefaultAsync(jp => jp.Id == id && jp.UserId == userId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<MyJobPreference>> GetByUserIdAndWorkModelAsync(string userId, WorkModel workModel, CancellationToken cancellationToken = default)
    {
        return await _context.MyJobPreferences
            .Include(jp => jp.MyProfile)
            .Where(jp => jp.UserId == userId && jp.WorkModel == workModel)
            .OrderBy(jp => jp.Title)
            .ThenByDescending(jp => jp.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<MyJobPreference>> GetByUserIdAndContractTypeAsync(string userId, ContractType contractType, CancellationToken cancellationToken = default)
    {
        return await _context.MyJobPreferences
            .Include(jp => jp.MyProfile)
            .Where(jp => jp.UserId == userId && jp.Contract == contractType)
            .OrderBy(jp => jp.Title)
            .ThenByDescending(jp => jp.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<MyJobPreference> AddAsync(MyJobPreference jobPreference, CancellationToken cancellationToken = default)
    {
        if (jobPreference == null)
            throw new ArgumentNullException(nameof(jobPreference));

        jobPreference.CreatedAt = DateTime.UtcNow;
        jobPreference.UpdatedAt = DateTime.UtcNow;

        _context.MyJobPreferences.Add(jobPreference);
        await _context.SaveChangesAsync(cancellationToken);

        // Reload with includes
        return await GetByIdAsync(jobPreference.Id, cancellationToken)
               ?? throw new InvalidOperationException("Failed to retrieve the created job preference");
    }

    /// <inheritdoc />
    public async Task<MyJobPreference> UpdateAsync(MyJobPreference jobPreference, CancellationToken cancellationToken = default)
    {
        if (jobPreference == null)
            throw new ArgumentNullException(nameof(jobPreference));

        var existingJobPreference = await _context.MyJobPreferences
            .FirstOrDefaultAsync(jp => jp.Id == jobPreference.Id, cancellationToken);

        if (existingJobPreference == null)
            throw new InvalidOperationException($"Job preference with ID {jobPreference.Id} not found");

        // Update properties
        existingJobPreference.MyProfileId = jobPreference.MyProfileId;
        existingJobPreference.Title = jobPreference.Title;
        existingJobPreference.WorkModel = jobPreference.WorkModel;
        existingJobPreference.Contract = jobPreference.Contract;
        existingJobPreference.OfferRelocation = jobPreference.OfferRelocation;
        existingJobPreference.OfferSponsorship = jobPreference.OfferSponsorship;
        existingJobPreference.TotalCompensation = jobPreference.TotalCompensation;
        existingJobPreference.Notes = jobPreference.Notes;
        existingJobPreference.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        // Reload with includes
        return await GetByIdAsync(existingJobPreference.Id, cancellationToken)
               ?? throw new InvalidOperationException("Failed to retrieve the updated job preference");
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id, string userId, CancellationToken cancellationToken = default)
    {
        var jobPreference = await _context.MyJobPreferences
            .FirstOrDefaultAsync(jp => jp.Id == id && jp.UserId == userId, cancellationToken);

        if (jobPreference == null)
            return false;

        _context.MyJobPreferences.Remove(jobPreference);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <inheritdoc />
    public async Task<int> GetCountByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _context.MyJobPreferences
            .CountAsync(jp => jp.UserId == userId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<int> GetCountByProfileIdAsync(int profileId, CancellationToken cancellationToken = default)
    {
        return await _context.MyJobPreferences
            .CountAsync(jp => jp.MyProfileId == profileId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<MyJobPreference>> GetPagedByUserIdAsync(string userId, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;

        return await _context.MyJobPreferences
            .Include(jp => jp.MyProfile)
            .Where(jp => jp.UserId == userId)
            .OrderBy(jp => jp.Title)
            .ThenByDescending(jp => jp.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}
