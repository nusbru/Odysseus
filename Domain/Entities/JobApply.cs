using System.ComponentModel.DataAnnotations;
using Odysseus.Domain.Enums;

namespace Odysseus.Domain.Entities;

/// <summary>
/// Represents a job application in the system
/// </summary>
public class JobApply
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Company Name is required")]
    [StringLength(200, ErrorMessage = "Company Name cannot exceed 200 characters")]
    public string CompanyName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Company Country is required")]
    [StringLength(100, ErrorMessage = "Company Country cannot exceed 100 characters")]
    public string CompanyCountry { get; set; } = string.Empty;

    [Required(ErrorMessage = "Job Role is required")]
    [StringLength(200, ErrorMessage = "Job Role cannot exceed 200 characters")]
    public string JobRole { get; set; } = string.Empty;

    [Url(ErrorMessage = "Please enter a valid URL")]
    [StringLength(500, ErrorMessage = "Job Link cannot exceed 500 characters")]
    public string? JobLink { get; set; }

    [Required(ErrorMessage = "Date of Apply is required")]
    public DateTime DateOfApply { get; set; }

    [Range(1, 10, ErrorMessage = "Number of phases must be between 1 and 10")]
    public int NumberOfPhases { get; set; } = 1;

    [Required(ErrorMessage = "Status is required")]
    public JobStatus Status { get; set; } = JobStatus.NotApplied;

    public bool RequiresSponsorship { get; set; }

    public bool RequiresRelocation { get; set; }

    [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
    public string? Notes { get; set; }

    // Navigation property to User
    [Required]
    public string UserId { get; set; } = string.Empty;

    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Validates the job application business rules
    /// </summary>
    public void ValidateBusinessRules()
    {
        if (DateOfApply > DateTime.UtcNow)
        {
            throw new InvalidOperationException("Date of Apply cannot be in the future");
        }

        if (Status == JobStatus.Applied && DateOfApply == default)
        {
            throw new InvalidOperationException("Date of Apply is required when status is Applied or later");
        }

        if (string.IsNullOrWhiteSpace(CompanyName))
        {
            throw new InvalidOperationException("Company Name is required");
        }

        if (string.IsNullOrWhiteSpace(JobRole))
        {
            throw new InvalidOperationException("Job Role is required");
        }
    }

    /// <summary>
    /// Updates the job application status
    /// </summary>
    public void UpdateStatus(JobStatus newStatus)
    {
        if (newStatus < Status && Status != JobStatus.InProgress)
        {
            throw new InvalidOperationException("Cannot move to a previous status except from In Progress");
        }

        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the job application as updated
    /// </summary>
    public void MarkAsUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
