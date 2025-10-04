using Odysseus.Host.Application.ViewModels;
using Odysseus.Host.Domain.Entities;

namespace Odysseus.Host.Application.Services;

/// <summary>
/// Service for mapping between domain entities and view models
/// </summary>
public static class MappingService
{
    /// <summary>
    /// Maps JobApply entity to JobApplyViewModel
    /// </summary>
    public static JobApplyViewModel ToViewModel(this JobApply entity)
    {
        return new JobApplyViewModel
        {
            Id = entity.Id,
            CompanyName = entity.CompanyName,
            CompanyCountry = entity.CompanyCountry,
            JobRole = entity.JobRole,
            JobLink = entity.JobLink,
            DateOfApply = entity.DateOfApply,
            NumberOfPhases = entity.NumberOfPhases,
            Status = entity.Status,
            OfferSponsorship = entity.OfferSponsorship,
            OfferRelocation = entity.OfferRelocation,
            Notes = entity.Notes,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    /// <summary>
    /// Maps collection of JobApply entities to JobApplyViewModels
    /// </summary>
    public static IEnumerable<JobApplyViewModel> ToViewModels(this IEnumerable<JobApply> entities)
    {
        return entities.Select(ToViewModel);
    }

    /// <summary>
    /// Maps JobApply entity to JobApplyFormViewModel for editing
    /// </summary>
    public static JobApplyFormViewModel ToFormViewModel(this JobApply entity)
    {
        return new JobApplyFormViewModel
        {
            Id = entity.Id,
            CompanyName = entity.CompanyName,
            CompanyCountry = entity.CompanyCountry,
            JobRole = entity.JobRole,
            JobLink = entity.JobLink,
            DateOfApply = entity.DateOfApply,
            NumberOfPhases = entity.NumberOfPhases,
            Status = entity.Status,
            OfferSponsorship = entity.OfferSponsorship,
            OfferRelocation = entity.OfferRelocation,
            Notes = entity.Notes
        };
    }

    /// <summary>
    /// Maps JobApplyFormViewModel to JobApply entity
    /// </summary>
    public static JobApply ToEntity(this JobApplyFormViewModel viewModel, string userId)
    {
        return new JobApply
        {
            Id = viewModel.Id,
            CompanyName = viewModel.CompanyName,
            CompanyCountry = viewModel.CompanyCountry,
            JobRole = viewModel.JobRole,
            JobLink = viewModel.JobLink,
            DateOfApply = viewModel.DateOfApply,
            NumberOfPhases = viewModel.NumberOfPhases,
            Status = viewModel.Status,
            OfferSponsorship = viewModel.OfferSponsorship,
            OfferRelocation = viewModel.OfferRelocation,
            Notes = viewModel.Notes,
            UserId = userId,
            CreatedAt = viewModel.Id == 0 ? DateTime.UtcNow : default, // Set only for new entities
            UpdatedAt = viewModel.Id != 0 ? DateTime.UtcNow : null // Set only for existing entities
        };
    }

    /// <summary>
    /// Updates an existing JobApply entity with values from JobApplyFormViewModel
    /// </summary>
    public static void UpdateFromFormViewModel(this JobApply entity, JobApplyFormViewModel viewModel)
    {
        entity.CompanyName = viewModel.CompanyName;
        entity.CompanyCountry = viewModel.CompanyCountry;
        entity.JobRole = viewModel.JobRole;
        entity.JobLink = viewModel.JobLink;
        entity.DateOfApply = viewModel.DateOfApply;
        entity.NumberOfPhases = viewModel.NumberOfPhases;
        entity.Status = viewModel.Status;
        entity.OfferSponsorship = viewModel.OfferSponsorship;
        entity.OfferRelocation = viewModel.OfferRelocation;
        entity.Notes = viewModel.Notes;
        entity.MarkAsUpdated();
    }

    /// <summary>
    /// Creates a DashboardViewModel from job applications
    /// </summary>
    public static DashboardViewModel ToDashboardViewModel(this IEnumerable<JobApply> entities)
    {
        var viewModels = entities.ToViewModels().ToList();
        var dashboard = new DashboardViewModel
        {
            JobApplications = viewModels
        };

        dashboard.CalculateStatistics();
        return dashboard;
    }

    #region MyProfile Mappings

    /// <summary>
    /// Maps MyProfile entity to MyProfileViewModel
    /// </summary>
    public static MyProfileViewModel ToViewModel(this MyProfile entity)
    {
        return new MyProfileViewModel
        {
            Id = entity.Id,
            UserId = entity.UserId,
            Passport = entity.Passport,
            NeedRelocationSupport = entity.NeedRelocationSupport,
            NeedSponsorship = entity.NeedSponsorship,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            JobPreferences = entity.JobPreferences.Select(jp => jp.ToViewModel()).ToList()
        };
    }

    /// <summary>
    /// Maps MyProfile entity to MyProfileFormViewModel for editing
    /// </summary>
    public static MyProfileFormViewModel ToFormViewModel(this MyProfile entity)
    {
        return new MyProfileFormViewModel
        {
            Id = entity.Id,
            Passport = entity.Passport,
            NeedRelocationSupport = entity.NeedRelocationSupport,
            NeedSponsorship = entity.NeedSponsorship
        };
    }

    /// <summary>
    /// Maps MyProfileFormViewModel to MyProfile entity
    /// </summary>
    public static MyProfile ToEntity(this MyProfileFormViewModel viewModel, string userId)
    {
        return new MyProfile
        {
            Id = viewModel.Id,
            UserId = userId,
            Passport = viewModel.Passport,
            NeedRelocationSupport = viewModel.NeedRelocationSupport,
            NeedSponsorship = viewModel.NeedSponsorship,
            CreatedAt = viewModel.Id == 0 ? DateTime.UtcNow : default,
            UpdatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Updates an existing MyProfile entity with values from MyProfileFormViewModel
    /// </summary>
    public static void UpdateFromFormViewModel(this MyProfile entity, MyProfileFormViewModel viewModel)
    {
        entity.Passport = viewModel.Passport;
        entity.NeedRelocationSupport = viewModel.NeedRelocationSupport;
        entity.NeedSponsorship = viewModel.NeedSponsorship;
        entity.MarkAsUpdated();
    }

    #endregion

    #region MyJobPreference Mappings

    /// <summary>
    /// Maps MyJobPreference entity to MyJobPreferenceViewModel
    /// </summary>
    public static MyJobPreferenceViewModel ToViewModel(this MyJobPreference entity)
    {
        return new MyJobPreferenceViewModel
        {
            Id = entity.Id,
            UserId = entity.UserId,
            MyProfileId = entity.MyProfileId,
            Title = entity.Title,
            WorkModel = entity.WorkModel,
            Contract = entity.Contract,
            OfferRelocation = entity.OfferRelocation,
            OfferSponsorship = entity.OfferSponsorship,
            TotalCompensation = entity.TotalCompensation,
            Notes = entity.Notes,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    /// <summary>
    /// Maps collection of MyJobPreference entities to MyJobPreferenceViewModels
    /// </summary>
    public static IEnumerable<MyJobPreferenceViewModel> ToJobPreferenceViewModels(this IEnumerable<MyJobPreference> entities)
    {
        return entities.Select(ToViewModel);
    }

    /// <summary>
    /// Maps MyJobPreference entity to MyJobPreferenceFormViewModel for editing
    /// </summary>
    public static MyJobPreferenceFormViewModel ToFormViewModel(this MyJobPreference entity)
    {
        return new MyJobPreferenceFormViewModel
        {
            Id = entity.Id,
            MyProfileId = entity.MyProfileId,
            Title = entity.Title,
            WorkModel = entity.WorkModel,
            Contract = entity.Contract,
            OfferRelocation = entity.OfferRelocation,
            OfferSponsorship = entity.OfferSponsorship,
            TotalCompensation = entity.TotalCompensation,
            Notes = entity.Notes
        };
    }

    /// <summary>
    /// Maps MyJobPreferenceFormViewModel to MyJobPreference entity
    /// </summary>
    public static MyJobPreference ToEntity(this MyJobPreferenceFormViewModel viewModel, string userId)
    {
        return new MyJobPreference
        {
            Id = viewModel.Id,
            UserId = userId,
            MyProfileId = viewModel.MyProfileId,
            Title = viewModel.Title,
            WorkModel = viewModel.WorkModel,
            Contract = viewModel.Contract,
            OfferRelocation = viewModel.OfferRelocation,
            OfferSponsorship = viewModel.OfferSponsorship,
            TotalCompensation = viewModel.TotalCompensation,
            Notes = viewModel.Notes,
            CreatedAt = viewModel.Id == 0 ? DateTime.UtcNow : default,
            UpdatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Updates an existing MyJobPreference entity with values from MyJobPreferenceFormViewModel
    /// </summary>
    public static void UpdateFromFormViewModel(this MyJobPreference entity, MyJobPreferenceFormViewModel viewModel)
    {
        entity.MyProfileId = viewModel.MyProfileId;
        entity.Title = viewModel.Title;
        entity.WorkModel = viewModel.WorkModel;
        entity.Contract = viewModel.Contract;
        entity.OfferRelocation = viewModel.OfferRelocation;
        entity.OfferSponsorship = viewModel.OfferSponsorship;
        entity.TotalCompensation = viewModel.TotalCompensation;
        entity.Notes = viewModel.Notes;
        entity.MarkAsUpdated();
    }

    #endregion
}
