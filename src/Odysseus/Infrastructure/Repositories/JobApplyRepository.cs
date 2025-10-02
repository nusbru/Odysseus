using Microsoft.EntityFrameworkCore;
using Odysseus.Host.Application.Interfaces;
using Odysseus.Host.Data;
using Odysseus.Host.Domain.Entities;
using Odysseus.Host.Domain.Enums;

namespace Odysseus.Host.Infrastructure.Repositories;

/// <summary>
/// Entity Framework implementation of IJobApplyRepository
/// </summary>
public class JobApplyRepository : IJobApplyRepository
{
    private readonly ApplicationDbContext _context;

    public JobApplyRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<JobApply>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _context.JobApplications
            .Where(j => j.UserId == userId)
            .OrderByDescending(j => j.DateOfApply)
            .ThenByDescending(j => j.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<JobApply?> GetByIdAndUserIdAsync(int id, string userId, CancellationToken cancellationToken = default)
    {
        return await _context.JobApplications
            .FirstOrDefaultAsync(j => j.Id == id && j.UserId == userId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<JobApply>> GetByUserIdAndStatusAsync(string userId, JobStatus status, CancellationToken cancellationToken = default)
    {
        return await _context.JobApplications
            .Where(j => j.UserId == userId && j.Status == status)
            .OrderByDescending(j => j.DateOfApply)
            .ThenByDescending(j => j.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<JobApply> AddAsync(JobApply jobApply, CancellationToken cancellationToken = default)
    {
        if (jobApply == null)
            throw new ArgumentNullException(nameof(jobApply));

        // Validate business rules before adding
        jobApply.ValidateBusinessRules();

        _context.JobApplications.Add(jobApply);
        await _context.SaveChangesAsync(cancellationToken);
        return jobApply;
    }

    /// <inheritdoc />
    public async Task<JobApply> UpdateAsync(JobApply jobApply, CancellationToken cancellationToken = default)
    {
        if (jobApply == null)
            throw new ArgumentNullException(nameof(jobApply));

        // Validate business rules before updating
        jobApply.ValidateBusinessRules();

        // Mark as updated
        jobApply.MarkAsUpdated();

        _context.JobApplications.Update(jobApply);
        await _context.SaveChangesAsync(cancellationToken);
        return jobApply;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id, string userId, CancellationToken cancellationToken = default)
    {
        var jobApply = await GetByIdAndUserIdAsync(id, userId, cancellationToken);
        if (jobApply == null)
            return false;

        _context.JobApplications.Remove(jobApply);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <inheritdoc />
    public async Task<int> GetCountByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _context.JobApplications
            .CountAsync(j => j.UserId == userId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<JobApply>> GetPagedByUserIdAsync(string userId, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;

        return await _context.JobApplications
            .Where(j => j.UserId == userId)
            .OrderByDescending(j => j.DateOfApply)
            .ThenByDescending(j => j.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}
