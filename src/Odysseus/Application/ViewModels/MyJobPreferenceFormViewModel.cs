using System.ComponentModel.DataAnnotations;
using Odysseus.Host.Domain.Enums;

namespace Odysseus.Host.Application.ViewModels;

/// <summary>
/// Form view model for creating and editing job preferences
/// </summary>
public class MyJobPreferenceFormViewModel
{
    public int Id { get; set; }

    public int MyProfileId { get; set; }

    [Required(ErrorMessage = "Job title is required")]
    [MaxLength(200, ErrorMessage = "Job title cannot exceed 200 characters")]
    [Display(Name = "Job Title")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Work model is required")]
    [Display(Name = "Work Model")]
    public WorkModel WorkModel { get; set; }

    [Required(ErrorMessage = "Contract type is required")]
    [Display(Name = "Contract Type")]
    public ContractType Contract { get; set; }

    [Display(Name = "Offers Relocation Support?")]
    public bool OfferRelocation { get; set; }

    [Display(Name = "Offers Sponsorship?")]
    public bool OfferSponsorship { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Total compensation must be a positive value")]
    [Display(Name = "Total Compensation")]
    public decimal? TotalCompensation { get; set; }

    [MaxLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
    [Display(Name = "Additional Notes")]
    public string? Notes { get; set; }

    /// <summary>
    /// Available work model options for dropdowns
    /// </summary>
    public static IEnumerable<WorkModelOption> WorkModelOptions => new[]
    {
        new WorkModelOption { Value = WorkModel.OnSite, Text = "On-Site" },
        new WorkModelOption { Value = WorkModel.Hybrid, Text = "Hybrid" },
        new WorkModelOption { Value = WorkModel.Remote, Text = "Remote" }
    };

    /// <summary>
    /// Available contract type options for dropdowns
    /// </summary>
    public static IEnumerable<ContractTypeOption> ContractTypeOptions => new[]
    {
        new ContractTypeOption { Value = ContractType.Permanent, Text = "Permanent" },
        new ContractTypeOption { Value = ContractType.FixedTerm, Text = "Fixed-term" },
        new ContractTypeOption { Value = ContractType.UnspecifiedDuration, Text = "Unspecified Duration" },
        new ContractTypeOption { Value = ContractType.PartTime, Text = "Part-time" },
        new ContractTypeOption { Value = ContractType.Temporary, Text = "Temporary" },
        new ContractTypeOption { Value = ContractType.ShortTerm, Text = "Short-term" },
        new ContractTypeOption { Value = ContractType.Freelance, Text = "Freelance" }
    };
}

/// <summary>
/// Helper class for work model dropdown options
/// </summary>
public class WorkModelOption
{
    public WorkModel Value { get; set; }
    public string Text { get; set; } = string.Empty;
}

/// <summary>
/// Helper class for contract type dropdown options
/// </summary>
public class ContractTypeOption
{
    public ContractType Value { get; set; }
    public string Text { get; set; } = string.Empty;
}
