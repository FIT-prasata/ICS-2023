using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Seeds;

namespace TimeTracker.DAL
{

    
    public class TimeTrackerDbContext: DbContext
    {
        private readonly bool _seedDemoData;

        public TimeTrackerDbContext(DbContextOptions contextOptions, bool seedDemoData = false)
            : base(contextOptions) =>
            _seedDemoData = seedDemoData;

        public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();
        public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<ProjectUserEntity> ProjectUsers => Set<ProjectUserEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ActivityEntity>()
                .HasOne(i => i.Assigned)
                .WithMany(i => i.Activities)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ActivityEntity>()
                .HasOne(i => i.CreatedBy)
                .WithMany(i => i.AuthoredActivities)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ActivityEntity>()
                .HasOne(i => i.Project)
                .WithMany(i => i.Activities)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectUserEntity>()
                .HasOne(i => i.ProjectEntity)
                .WithMany(i => i.Users)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectUserEntity>()
                .HasOne(i => i.UserEntity)
                .WithMany(i => i.Projects)
                .OnDelete(DeleteBehavior.Cascade);

            if (_seedDemoData)
            {
                UserSeeds.Seed(modelBuilder);
                ProjectSeeds.Seed(modelBuilder);
                ActivitySeeds.Seed(modelBuilder);
            }
        }

    }
}
