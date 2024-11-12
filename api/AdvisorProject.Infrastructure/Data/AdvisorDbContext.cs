using AdvisorProject.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvisorProject.Infrastructure.Data;
public class AdvisorDbContext : DbContext
{
    public AdvisorDbContext(DbContextOptions<AdvisorDbContext> options) : base(options) { }

    public DbSet<Advisor> Advisors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Advisor>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.FullName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasIndex(a => a.SIN)
                    .IsUnique();

                entity.Property(a => a.SIN)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsFixedLength();

                entity.Property(a => a.Address)
                    .HasMaxLength(255);

                entity.Property(a => a.PhoneNumber)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(a => a.HealthStatus);
            });
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is EntityBase && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (EntityBase)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }

            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}