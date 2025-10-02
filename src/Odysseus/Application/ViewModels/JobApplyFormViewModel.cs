using System.ComponentModel.DataAnnotations;
using Odysseus.Host.Domain.Enums;
using Odysseus.Host.Domain.Entities;

namespace Odysseus.Host.Application.ViewModels;

/// <summary>
/// ViewModel for creating and editing job applications
/// </summary>
public class JobApplyFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Company Name is required")]
    [StringLength(200, ErrorMessage = "Company Name cannot exceed 200 characters")]
    [Display(Name = "Company Name")]
    public string CompanyName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Company Country is required")]
    [StringLength(100, ErrorMessage = "Company Country cannot exceed 100 characters")]
    [Display(Name = "Company Country")]
    public string CompanyCountry { get; set; } = string.Empty;

    [Required(ErrorMessage = "Job Role is required")]
    [StringLength(200, ErrorMessage = "Job Role cannot exceed 200 characters")]
    [Display(Name = "Job Role")]
    public string JobRole { get; set; } = string.Empty;

    [Url(ErrorMessage = "Please enter a valid URL")]
    [StringLength(500, ErrorMessage = "Job Link cannot exceed 500 characters")]
    [Display(Name = "Job Link (Optional)")]
    public string? JobLink { get; set; }

    [Required(ErrorMessage = "Date of Apply is required")]
    [DataType(DataType.Date)]
    [Display(Name = "Date of Apply")]
    public DateTime DateOfApply { get; set; } = DateTime.Today;

    [Range(1, 10, ErrorMessage = "Number of phases must be between 1 and 10")]
    [Display(Name = "Number of Phases")]
    public int NumberOfPhases { get; set; } = 1;

    [Required(ErrorMessage = "Status is required")]
    [Display(Name = "Status")]
    public JobStatus Status { get; set; } = JobStatus.NotApplied;

    [Display(Name = "Requires Sponsorship")]
    public bool RequiresSponsorship { get; set; }

    [Display(Name = "Requires Relocation")]
    public bool RequiresRelocation { get; set; }

    [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
    [Display(Name = "Notes (Optional)")]
    [DataType(DataType.MultilineText)]
    public string? Notes { get; set; }

    /// <summary>
    /// Gets whether this is a new job application (Id is 0)
    /// </summary>
    public bool IsNew => Id == 0;

    /// <summary>
    /// Gets the form title based on whether it's new or edit
    /// </summary>
    public string FormTitle => IsNew ? "Add New Job Application" : "Edit Job Application";

    /// <summary>
    /// Gets the submit button text
    /// </summary>
    public string SubmitButtonText => IsNew ? "Add Application" : "Update Application";

    /// <summary>
    /// Validates that the date is not in the future
    /// </summary>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DateOfApply > DateTime.Today)
        {
            yield return new ValidationResult(
                "Date of Apply cannot be in the future",
                new[] { nameof(DateOfApply) });
        }

        if (Status != JobStatus.NotApplied && DateOfApply == default)
        {
            yield return new ValidationResult(
                "Date of Apply is required when status is Applied or later",
                new[] { nameof(DateOfApply) });
        }
    }
}
