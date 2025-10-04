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
}
