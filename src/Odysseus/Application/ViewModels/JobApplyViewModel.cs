using System.ComponentModel.DataAnnotations;
using Odysseus.Host.Domain.Enums;

namespace Odysseus.Host.Application.ViewModels;

/// <summary>
/// ViewModel for displaying job application details
/// </summary>
public class JobApplyViewModel
{
    public int Id { get; set; }

    [Display(Name = "Company Name")]
    public string CompanyName { get; set; } = string.Empty;

    [Display(Name = "Company Country")]
    public string CompanyCountry { get; set; } = string.Empty;

    [Display(Name = "Job Role")]
    public string JobRole { get; set; } = string.Empty;

    [Display(Name = "Job Link")]
    public string? JobLink { get; set; }

    [Display(Name = "Date of Apply")]
    [DataType(DataType.Date)]
    public DateTime DateOfApply { get; set; }

    [Display(Name = "Number of Phases")]
    public int NumberOfPhases { get; set; }

    [Display(Name = "Status")]
    public JobStatus Status { get; set; }

    [Display(Name = "Status Display")]
    public string StatusDisplay => GetStatusDisplay(Status);

    [Display(Name = "Offer Sponsorship")]
    public bool OfferSponsorship { get; set; }

    [Display(Name = "Offer Relocation")]
    public bool OfferRelocation { get; set; }

    [Display(Name = "Notes")]
    public string? Notes { get; set; }

    [Display(Name = "Created At")]
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Updated At")]
    [DataType(DataType.DateTime)]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets the CSS class for the status badge
    /// </summary>
    public string StatusBadgeClass => GetStatusBadgeClass(Status);

    /// <summary>
    /// Gets the display text for job status
    /// </summary>
    private static string GetStatusDisplay(JobStatus status) => status switch
    {
        JobStatus.NotApplied => "Not Applied",
        JobStatus.Applied => "Applied",
        JobStatus.InProgress => "In Progress",
        JobStatus.WaitingResponse => "Waiting Response",
        JobStatus.WaitingJobOffer => "Waiting Job Offer",
        JobStatus.Accepted => "Accepted",
        JobStatus.Denied => "Denied",
        JobStatus.Failed => "Failed",
        _ => status.ToString()
    };

    /// <summary>
    /// Gets the Bootstrap CSS class for status badges
    /// </summary>
    private static string GetStatusBadgeClass(JobStatus status) => status switch
    {
        JobStatus.NotApplied => "badge bg-secondary",
        JobStatus.Applied => "badge bg-primary",
        JobStatus.InProgress => "badge bg-info",
        JobStatus.WaitingResponse => "badge bg-warning",
        JobStatus.WaitingJobOffer => "badge bg-warning",
        JobStatus.Accepted => "badge bg-success",
        JobStatus.Denied => "badge bg-danger",
        JobStatus.Failed => "badge bg-danger",
        _ => "badge bg-secondary"
    };

    /// <summary>
    /// Gets days since application
    /// </summary>
    public int DaysSinceApply => (DateTime.Now - DateOfApply).Days;

    /// <summary>
    /// Gets whether this application is recent (within 7 days)
    /// </summary>
    public bool IsRecent => DaysSinceApply <= 7;
}
