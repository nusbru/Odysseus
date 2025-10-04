using Odysseus.Host.Domain.Entities;

namespace Odysseus.Host.Application.Interfaces;

/// <summary>
/// Repository interface for MyProfile entity operations
/// </summary>
public interface IMyProfileRepository
{
    /// <summary>
    /// Gets the profile for a specific user
    /// </summary>
    /// <param name="userId">The user's ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user's profile if exists, null otherwise</returns>
    Task<MyProfile?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a profile by ID
    /// </summary>
    /// <param name="id">The profile ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The profile if found, null otherwise</returns>
    Task<MyProfile?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a profile by ID for a specific user (security check)
    /// </summary>
    /// <param name="id">The profile ID</param>
    /// <param name="userId">The user's ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The profile if found and belongs to user, null otherwise</returns>
    Task<MyProfile?> GetByIdAndUserIdAsync(int id, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new profile
    /// </summary>
    /// <param name="profile">The profile to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created profile</returns>
    Task<MyProfile> AddAsync(MyProfile profile, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing profile
    /// </summary>
    /// <param name="profile">The profile to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated profile</returns>
    Task<MyProfile> UpdateAsync(MyProfile profile, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a profile
    /// </summary>
    /// <param name="id">The ID of the profile to delete</param>
    /// <param name="userId">The user's ID (for security)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if deleted successfully, false if not found or not owned by user</returns>
    Task<bool> DeleteAsync(int id, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a user already has a profile
    /// </summary>
    /// <param name="userId">The user's ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if profile exists, false otherwise</returns>
    Task<bool> ExistsForUserAsync(string userId, CancellationToken cancellationToken = default);
}
