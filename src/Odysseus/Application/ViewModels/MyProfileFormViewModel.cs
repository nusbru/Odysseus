using System.ComponentModel.DataAnnotations;

namespace Odysseus.Host.Application.ViewModels;

/// <summary>
/// Form view model for creating and editing MyProfile
/// </summary>
public class MyProfileFormViewModel
{
    public int Id { get; set; }

    [MaxLength(50, ErrorMessage = "Passport number cannot exceed 50 characters")]
    [Display(Name = "Passport Number")]
    public string? Passport { get; set; }

    [Display(Name = "Need Relocation Support?")]
    public bool? NeedRelocationSupport { get; set; }

    [Display(Name = "Need Sponsorship?")]
    public bool? NeedSponsorship { get; set; }

    /// <summary>
    /// Helper property for handling nullable bool in forms
    /// </summary>
    public string RelocationSupportSelection
    {
        get => NeedRelocationSupport switch
        {
            true => "yes",
            false => "no",
            null => "not_specified"
        };
        set => NeedRelocationSupport = value switch
        {
            "yes" => true,
            "no" => false,
            _ => null
        };
    }

    /// <summary>
    /// Helper property for handling nullable bool in forms
    /// </summary>
    public string SponsorshipSelection
    {
        get => NeedSponsorship switch
        {
            true => "yes",
            false => "no",
            null => "not_specified"
        };
        set => NeedSponsorship = value switch
        {
            "yes" => true,
            "no" => false,
            _ => null
        };
    }
}
