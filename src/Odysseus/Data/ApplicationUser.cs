using Microsoft.AspNetCore.Identity;
using Odysseus.Host.Domain.Entities;

namespace Odysseus.Host.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Collection of job applications for this user
    /// </summary>
    public virtual ICollection<JobApply> JobApplications { get; set; } = new List<JobApply>();

    /// <summary>
    /// User's profile information
    /// </summary>
    public virtual MyProfile? MyProfile { get; set; }

    /// <summary>
    /// Collection of job preferences for this user
    /// </summary>
    public virtual ICollection<MyJobPreference> JobPreferences { get; set; } = new List<MyJobPreference>();

    /// <summary>
    /// User's preferred name for display
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// When the user account was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// When the user last updated their profile
    /// </summary>
    public DateTime? LastUpdatedAt { get; set; }
}

