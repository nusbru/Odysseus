using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Odysseus.Domain.Entities;

namespace Odysseus.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<JobApply> JobApplications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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

        // Configure ApplicationUser additional properties
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100);

            entity.Property(e => e.CreatedAt)
                .IsRequired();
        });
    }
}
