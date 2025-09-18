using Odysseus.Domain.Entities;
using Odysseus.Domain.Enums;

namespace Odysseus.Application.Interfaces;

/// <summary>
/// Repository interface for JobApply entity operations
/// </summary>
public interface IJobApplyRepository
{
    /// <summary>
    /// Gets all job applications for a specific user
    /// </summary>
    /// <param name="userId">The user's ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of job applications</returns>
    Task<IEnumerable<JobApply>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific job application by ID for a user
    /// </summary>
    /// <param name="id">The job application ID</param>
    /// <param name="userId">The user's ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The job application if found and belongs to user, null otherwise</returns>
    Task<JobApply?> GetByIdAndUserIdAsync(int id, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets job applications for a user filtered by status
    /// </summary>
    /// <param name="userId">The user's ID</param>
    /// <param name="status">The status to filter by</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of job applications with the specified status</returns>
    Task<IEnumerable<JobApply>> GetByUserIdAndStatusAsync(string userId, JobStatus status, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new job application
    /// </summary>
    /// <param name="jobApply">The job application to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The added job application</returns>
    Task<JobApply> AddAsync(JobApply jobApply, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing job application
    /// </summary>
    /// <param name="jobApply">The job application to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated job application</returns>
    Task<JobApply> UpdateAsync(JobApply jobApply, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a job application
    /// </summary>
    /// <param name="id">The ID of the job application to delete</param>
    /// <param name="userId">The user's ID (for security)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if deleted successfully, false if not found or not owned by user</returns>
    Task<bool> DeleteAsync(int id, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the count of job applications for a user
    /// </summary>
    /// <param name="userId">The user's ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The count of job applications</returns>
    Task<int> GetCountByUserIdAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets job applications for a user with pagination
    /// </summary>
    /// <param name="userId">The user's ID</param>
    /// <param name="page">Page number (1-based)</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paginated collection of job applications</returns>
    Task<IEnumerable<JobApply>> GetPagedByUserIdAsync(string userId, int page, int pageSize, CancellationToken cancellationToken = default);
}