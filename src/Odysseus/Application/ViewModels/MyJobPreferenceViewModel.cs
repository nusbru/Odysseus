using System.ComponentModel.DataAnnotations;
using Odysseus.Host.Domain.Enums;

namespace Odysseus.Host.Application.ViewModels;

/// <summary>
/// View model for displaying job preference information
/// </summary>
public class MyJobPreferenceViewModel
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public int MyProfileId { get; set; }

    public string Title { get; set; } = string.Empty;

    public WorkModel WorkModel { get; set; }

    public ContractType Contract { get; set; }

    public bool OfferRelocation { get; set; }

    public bool OfferSponsorship { get; set; }

    public decimal? TotalCompensation { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Display text for work model
    /// </summary>
    public string WorkModelText => WorkModel switch
    {
        WorkModel.OnSite => "On-Site",
        WorkModel.Hybrid => "Hybrid",
        WorkModel.Remote => "Remote",
        _ => "Unknown"
    };

    /// <summary>
    /// Display text for contract type
    /// </summary>
    public string ContractText => Contract switch
    {
        ContractType.Permanent => "Permanent",
        ContractType.FixedTerm => "Fixed-term",
        ContractType.UnspecifiedDuration => "Unspecified Duration",
        ContractType.PartTime => "Part-time",
        ContractType.Temporary => "Temporary",
        ContractType.ShortTerm => "Short-term",
        ContractType.Freelance => "Freelance",
        _ => "Unknown"
    };

    /// <summary>
    /// Formatted compensation text
    /// </summary>
    public string CompensationText => TotalCompensation.HasValue
        ? $"{TotalCompensation:C}"
        : "Not specified";

    /// <summary>
    /// Display text for relocation offer
    /// </summary>
    public string RelocationText => OfferRelocation ? "Yes" : "No";

    /// <summary>
    /// Display text for sponsorship offer
    /// </summary>
    public string SponsorshipText => OfferSponsorship ? "Yes" : "No";
}
