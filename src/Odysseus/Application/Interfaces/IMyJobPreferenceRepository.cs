using Odysseus.Host.Domain.Entities;
using Odysseus.Host.Domain.Enums;

namespace Odysseus.Host.Application.Interfaces;

/// <summary>
/// Repository interface for MyJobPreference entity operations
/// </summary>
public interface IMyJobPreferenceRepository
{
    /// <summary>
    /// Gets all job preferences for a specific user
    /// </summary>
    /// <param name="userId">The user's ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of job preferences</returns>
    Task<IEnumerable<MyJobPreference>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all job preferences for a specific profile
    /// </summary>
    /// <param name="profileId">The profile ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of job preferences</returns>
    Task<IEnumerable<MyJobPreference>> GetByProfileIdAsync(int profileId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific job preference by ID
    /// </summary>
    /// <param name="id">The job preference ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The job preference if found, null otherwise</returns>
    Task<MyJobPreference?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific job preference by ID for a user (security check)
    /// </summary>
    /// <param name="id">The job preference ID</param>
    /// <param name="userId">The user's ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The job preference if found and belongs to user, null otherwise</returns>
    Task<MyJobPreference?> GetByIdAndUserIdAsync(int id, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets job preferences for a user filtered by work model
    /// </summary>
    /// <param name="userId">The user's ID</param>
    /// <param name="workModel">The work model to filter by</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of job preferences with the specified work model</returns>
    Task<IEnumerable<MyJobPreference>> GetByUserIdAndWorkModelAsync(string userId, WorkModel workModel, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets job preferences for a user filtered by contract type
    /// </summary>
    /// <param name="userId">The user's ID</param>
    /// <param name="contractType">The contract type to filter by</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Collection of job preferences with the specified contract type</returns>
    Task<IEnumerable<MyJobPreference>> GetByUserIdAndContractTypeAsync(string userId, ContractType contractType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new job preference
    /// </summary>
    /// <param name="jobPreference">The job preference to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The added job preference</returns>
    Task<MyJobPreference> AddAsync(MyJobPreference jobPreference, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing job preference
    /// </summary>
    /// <param name="jobPreference">The job preference to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated job preference</returns>
    Task<MyJobPreference> UpdateAsync(MyJobPreference jobPreference, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a job preference
    /// </summary>
    /// <param name="id">The ID of the job preference to delete</param>
    /// <param name="userId">The user's ID (for security)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if deleted successfully, false if not found or not owned by user</returns>
    Task<bool> DeleteAsync(int id, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the count of job preferences for a user
    /// </summary>
    /// <param name="userId">The user's ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The count of job preferences</returns>
    Task<int> GetCountByUserIdAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the count of job preferences for a profile
    /// </summary>
    /// <param name="profileId">The profile ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The count of job preferences</returns>
    Task<int> GetCountByProfileIdAsync(int profileId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets job preferences for a user with pagination
    /// </summary>
    /// <param name="userId">The user's ID</param>
    /// <param name="page">Page number (1-based)</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paginated collection of job preferences</returns>
    Task<IEnumerable<MyJobPreference>> GetPagedByUserIdAsync(string userId, int page, int pageSize, CancellationToken cancellationToken = default);
}
