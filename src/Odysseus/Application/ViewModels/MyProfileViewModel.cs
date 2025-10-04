using System.ComponentModel.DataAnnotations;

namespace Odysseus.Host.Application.ViewModels;

/// <summary>
/// View model for displaying MyProfile information
/// </summary>
public class MyProfileViewModel
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string? Passport { get; set; }

    public bool? NeedRelocationSupport { get; set; }

    public bool? NeedSponsorship { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Collection of job preferences for this profile
    /// </summary>
    public ICollection<MyJobPreferenceViewModel> JobPreferences { get; set; } = new List<MyJobPreferenceViewModel>();

    /// <summary>
    /// Count of job preferences
    /// </summary>
    public int JobPreferencesCount => JobPreferences.Count;

    /// <summary>
    /// Display text for relocation support
    /// </summary>
    public string RelocationSupportText => NeedRelocationSupport switch
    {
        true => "Yes",
        false => "No",
        null => "Not specified"
    };

    /// <summary>
    /// Display text for sponsorship need
    /// </summary>
    public string SponsorshipText => NeedSponsorship switch
    {
        true => "Yes",
        false => "No",
        null => "Not specified"
    };
}
