using Odysseus.Host.Application.Services;
using Odysseus.Host.Application.ViewModels;
using Odysseus.Host.Domain.Entities;
using Odysseus.Host.Domain.Enums;

namespace Odysseus.Host.Application.Tests;

/// <summary>
/// Simple test class to verify mapping functionality
/// Note: This would normally be in a separate test project
/// </summary>
public static class MappingTests
{
    /// <summary>
    /// Test MyProfile mappings
    /// </summary>
    public static void TestMyProfileMappings()
    {
        // Create test entity
        var profile = new MyProfile
        {
            Id = 1,
            UserId = "test-user-123",
            Passport = "ABC123456",
            NeedRelocationSupport = true,
            NeedSponsorship = false,
            CreatedAt = DateTime.UtcNow.AddDays(-10),
            UpdatedAt = DateTime.UtcNow
        };

        // Test entity to ViewModel mapping
        var viewModel = profile.ToViewModel();
        Console.WriteLine($"Profile ViewModel: {viewModel.Id}, {viewModel.Passport}, {viewModel.RelocationSupportText}");

        // Test entity to FormViewModel mapping
        var formViewModel = profile.ToFormViewModel();
        Console.WriteLine($"Profile FormViewModel: {formViewModel.Id}, {formViewModel.Passport}, {formViewModel.RelocationSupportSelection}");

        // Test FormViewModel to entity mapping
        var newEntity = formViewModel.ToEntity("test-user-456");
        Console.WriteLine($"New Entity: {newEntity.UserId}, {newEntity.Passport}, {newEntity.NeedRelocationSupport}");
    }

    /// <summary>
    /// Test MyJobPreference mappings
    /// </summary>
    public static void TestMyJobPreferenceMappings()
    {
        // Create test entity
        var jobPreference = new MyJobPreference
        {
            Id = 1,
            UserId = "test-user-123",
            MyProfileId = 1,
            Title = "Senior Developer",
            WorkModel = WorkModel.Hybrid,
            Contract = ContractType.Permanent,
            OfferRelocation = true,
            OfferSponsorship = false,
            TotalCompensation = 75000m,
            Notes = "Looking for challenging opportunities",
            CreatedAt = DateTime.UtcNow.AddDays(-5),
            UpdatedAt = DateTime.UtcNow
        };

        // Test entity to ViewModel mapping
        var viewModel = jobPreference.ToViewModel();
        Console.WriteLine($"JobPreference ViewModel: {viewModel.Title}, {viewModel.WorkModelText}, {viewModel.CompensationText}");

        // Test entity to FormViewModel mapping
        var formViewModel = jobPreference.ToFormViewModel();
        Console.WriteLine($"JobPreference FormViewModel: {formViewModel.Title}, {formViewModel.WorkModel}, {formViewModel.TotalCompensation}");

        // Test FormViewModel to entity mapping
        var newEntity = formViewModel.ToEntity("test-user-456");
        Console.WriteLine($"New Entity: {newEntity.UserId}, {newEntity.Title}, {newEntity.WorkModel}");
    }
}
