using Microsoft.EntityFrameworkCore;
using Tracker.DAL.Entities;
using Tracker.DAL.Seeds;

namespace Tracker.DAL;
public class TrackerDbContext : DbContext
{
    private readonly bool _seedDemoData;

    public TrackerDbContext(DbContextOptions contextOptions, bool seedDemoData = false)
        : base(contextOptions) => _seedDemoData = seedDemoData;
     
    public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();
    public DbSet<ActivityProjectEntity> ActivityProjectEntities => Set<ActivityProjectEntity>();
    public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
    public DbSet<ProjectUserEntity> ProjectsUserEntities => Set<ProjectUserEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>()
            .HasMany(i => i.Activities);
        modelBuilder.Entity<UserEntity>()
            .HasMany(i => i.Projects)
            .WithOne(i => i.User)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<ProjectEntity>()
            .HasMany(i => i.Users)
            .WithOne(i => i.Project)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<ProjectEntity>()
            .HasMany(i => i.Activities)
            .WithOne(i => i.Project)
            .OnDelete(DeleteBehavior.Restrict);

        if (_seedDemoData)
        {
            ActivitySeeds.Seed(modelBuilder);
            ProjectSeeds.Seed(modelBuilder);
            UserSeeds.Seed(modelBuilder);
        }
    }
}
