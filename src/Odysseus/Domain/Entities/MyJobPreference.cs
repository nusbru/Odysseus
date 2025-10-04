using System.ComponentModel.DataAnnotations;
using Odysseus.Host.Domain.Enums;
using Odysseus.Host.Data;

namespace Odysseus.Host.Domain.Entities
{
    public class MyJobPreference
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int MyProfileId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public WorkModel WorkModel { get; set; }

        [Required]
        public ContractType Contract { get; set; }

        public bool OfferRelocation { get; set; }

        public bool OfferSponsorship { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? TotalCompensation { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        // Navigation properties
        public ApplicationUser User { get; set; } = null!;
        public MyProfile MyProfile { get; set; } = null!;

        // Audit properties
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Marks the entity as updated by setting UpdatedAt to current UTC time
        /// </summary>
        public void MarkAsUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
