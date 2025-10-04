using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Odysseus.Host.Domain.Entities;

namespace Odysseus.Host.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<JobApply> JobApplications { get; set; }
    public DbSet<MyProfile> MyProfiles { get; set; }
    public DbSet<MyJobPreference> MyJobPreferences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure DateTime conversion for PostgreSQL
        ConfigureDateTimeConversions(modelBuilder);

        // Configure JobApply entity
        modelBuilder.Entity<JobApply>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.CompanyName)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.CompanyCountry)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.JobRole)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.JobLink)
                .HasMaxLength(500);

            entity.Property(e => e.Notes)
                .HasMaxLength(1000);

            entity.Property(e => e.UserId)
                .IsRequired();

            entity.Property(e => e.Status)
                .IsRequired()
                .HasConversion<int>();

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            // Configure relationship with ApplicationUser
            entity.HasOne<ApplicationUser>()
                .WithMany(u => u.JobApplications)
                .HasForeignKey(j => j.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Add index for better query performance
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.DateOfApply);
        });

        // Configure MyProfile entity
        modelBuilder.Entity<MyProfile>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.UserId)
                .IsRequired();

            entity.Property(e => e.Passport)
                .HasMaxLength(50);

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();

            // Configure relationship with ApplicationUser
            entity.HasOne(p => p.User)
                .WithOne(u => u.MyProfile)
                .HasForeignKey<MyProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Add index for better query performance
            entity.HasIndex(e => e.UserId).IsUnique();
        });

        // Configure MyJobPreference entity
        modelBuilder.Entity<MyJobPreference>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.UserId)
                .IsRequired();

            entity.Property(e => e.MyProfileId)
                .IsRequired();

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.WorkModel)
                .IsRequired()
                .HasConversion<int>();

            entity.Property(e => e.Contract)
                .IsRequired()
                .HasConversion<int>();

            entity.Property(e => e.TotalCompensation)
                .HasPrecision(18, 2);

            entity.Property(e => e.Notes)
                .HasMaxLength(1000);

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();

            // Configure relationship with ApplicationUser
            entity.HasOne(jp => jp.User)
                .WithMany(u => u.JobPreferences)
                .HasForeignKey(jp => jp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationship with MyProfile
            entity.HasOne(jp => jp.MyProfile)
                .WithMany(p => p.JobPreferences)
                .HasForeignKey(jp => jp.MyProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // Add indexes for better query performance
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.MyProfileId);
            entity.HasIndex(e => new { e.UserId, e.Title });
        });

        // Configure ApplicationUser additional properties
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100);

            entity.Property(e => e.CreatedAt)
                .IsRequired();
        });
    }

    /// <summary>
    /// Configures DateTime conversions for PostgreSQL compatibility
    /// </summary>
    private void ConfigureDateTimeConversions(ModelBuilder modelBuilder)
    {
        // Create a value converter that ensures DateTime values are stored as UTC
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(v, DateTimeKind.Utc) : v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
            v => v.HasValue
                ? (v.Value.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v.Value.ToUniversalTime())
                : v,
            v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

        // Apply the converter to all DateTime properties
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(dateTimeConverter);
                }
                else if (property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(nullableDateTimeConverter);
                }
            }
        }
    }
}
