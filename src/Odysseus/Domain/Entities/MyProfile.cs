using System.ComponentModel.DataAnnotations;
using Odysseus.Host.Data;

namespace Odysseus.Host.Domain.Entities
{
    public class MyProfile
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Passport { get; set; }

        public bool? NeedRelocationSupport { get; set; }

        public bool? NeedSponsorship { get; set; }

        // Navigation properties
        public ApplicationUser User { get; set; } = null!;
        public ICollection<MyJobPreference> JobPreferences { get; set; } = new List<MyJobPreference>();

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
